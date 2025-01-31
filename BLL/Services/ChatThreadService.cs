using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BLL.Services
{
    public class ChatThreadService : Service, IService<ChatThread, ChatThreadModel>
    {
        // Public constructor.
        public ChatThreadService(Db db) : base(db)
        {
        }
        
        // CREATE METHOD.
        // Create new Record method.
        public Service Create(ChatThread record)
        {
            // Remove this condition, because uniqueness is 
            // provided by new Guid everytime.
            if (_db.ChatThreads.Any(x => x.Title == record.Title))
                return Error("Chat thread already exists.");
            
            record.Id = Guid.NewGuid();         // It is better to generate Guids in the service layer.
            record.Title = record.Title;        // Set the title of the chat thread.
            record.CreatedAt = DateTime.UtcNow; // Set the timestamp for when the chat thread was created.

            _db.ChatThreads.Add(record);
            _db.SaveChanges();
            return Success("Chat thread created successfully.");
        }
        
        // UPDATE METHOD
        // In this Update method, users can only update the 
        // title of the chat thread.
        public Service Update(ChatThread record)
        {
            var chatThread = _db.ChatThreads.Find(record.Title);
            if (chatThread == null)
                return Error("Chat thread not found.");
            
            // Chat thread is not null.
            chatThread.Title = record.Title;
            _db.ChatThreads.Update(chatThread);
            _db.SaveChanges();
            return Success("Chat thread updated successfully.");
            
        }
        
        
        // DELETE METHOD
        public Service Delete(Guid id)
        {
            // I find this way more convenient to find the Record.
            var record = _db.ChatThreads.Find(id);
            if (record == null)
                return Error("Chat thread not found.");
            
            _db.ChatThreads.Remove(record);
            _db.SaveChanges();
            return Success("Chat thread deleted successfully.");
        }
        
        
        public IQueryable<ChatThreadModel> Query()
        {
            return _db.ChatThreads.Select(x => new ChatThreadModel()
            {
                Record = x,
                Id = x.Id,
                Title = x.Title,
                CreatedAt = x.CreatedAt
            });
        }
    }
}