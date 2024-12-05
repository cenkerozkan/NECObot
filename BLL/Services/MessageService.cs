using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BLL.Services
{
    public class MessagesService : Service, IService<Message, MessageModel>
    {
        // Constructor.
        public MessagesService(Db db) : base(db)
        {
        }

        public Service Create(Message record)
        {
            record.Id = Guid.NewGuid();
            record.Timestamp = DateTime.UtcNow;
            _db.Messages.Add(record);
            _db.SaveChanges();
            return Success("Message created successfully.");
        }

        // I know this is a bad approach, but I did not want to
        // corrupt the IService structure so I implemented it this way.
        public Service Update(Message record)
        {
            return Error("This is method is not implemented.");
        }

        public Service Delete(Guid id)
        {
            return Error("This is method is not implemented.");
        }


        public IQueryable<MessageModel> Query()
        {
            return _db.Messages.Select(x => new MessageModel()
            {
                Record = x,
                Id = x.Id,
                Timestamp = x.Timestamp,
                Content = x.Content,
                Role = x.Role,
                ChatThreadId = x.ChatThreadId,
                ChatThread = x.ChatThread
            });
        }
    }
}