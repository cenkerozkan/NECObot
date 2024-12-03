using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DAL
{
    [Table("messages")]
    public class Message
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // Use UUID as primary key
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Timestamp for when the message was created
        public string Content { get; set; } = string.Empty; // Content of the message
        public string Role { get; set; } = string.Empty; // Role of the sender (e.g., user, bot)

        [ForeignKey("ChatThread")]
        public Guid ChatThreadId { get; set; } // Foreign key referencing ChatThread
        public ChatThread ChatThread { get; set; } // Navigation property
    }
}