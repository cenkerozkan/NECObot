using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class ChatThread
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // Use UUID as primary key
        public string Title { get; set; } = string.Empty; // Title of the chat thread
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for when the chat thread was created

        public ICollection<Message> Messages { get; set; } = new List<Message>(); // One-to-many relationship with messages
    }
}