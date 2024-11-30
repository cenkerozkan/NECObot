#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DAL
{
    public class AcceptedMessage
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();               // UUIDs are better to use as primary keys
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }          
        public string Role { get; set; }             
        public string AcceptedBy { get; set; }       
        public DateTime AcceptedTimestamp { get; set; } 

        [ForeignKey("Message")]
        public Guid MessageId { get; set; } 
        public Message Message { get; set; }
    }
}