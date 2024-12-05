#nullable disable

using BLL.DAL;
using System.ComponentModel;
using System.Collections.Generic;

namespace BLL.Models
{
    public class MessageModel
    {
        public Message Record { get; set; }
        public Guid Id { get; set; }               
        public DateTime Timestamp { get; set; }    
        public string Content { get; set; }        
        public string Role { get; set; }           
        public Guid ChatThreadId { get; set; }     
        public ChatThread ChatThread { get; set; } 
    }
}