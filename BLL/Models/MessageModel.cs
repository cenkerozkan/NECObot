#nullable disable

using BLL.DAL;
using System.ComponentModel;
using System.Collections.Generic;

namespace BLL.Models
{
    public class MessageModel
    {
        public Message Record { get; set; }
        public Message Id { get; set; }             // Guid
        public Message Timestamp { get; set; }      // DateTime
        public Message Content { get; set; }        // String
        public Message Role { get; set; }           // String
        public Message ChatThreadId { get; set; }   // Guid
        public Message ChatThread { get; set; }     // Navigation property
    }
}