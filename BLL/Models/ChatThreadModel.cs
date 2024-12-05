#nullable disable

using BLL.DAL;
using System.ComponentModel;
using System.Collections.Generic;

namespace BLL.Models
{
    public class ChatThreadModel
    {
        public ChatThread Record { get; set; }
        public Guid Id { get; set; }          // Guid
        public string Title { get; set; }       // String
        public DateTime CreatedAt { get; set; }   // DateTime
    }
}