#nullable disable

using BLL.DAL;
using System.ComponentModel;
using System.Collections.Generic;

namespace BLL.Models
{
    public class ChatThreadModel
    {
        public ChatThread Id { get; set; }          // Guid
        public ChatThread Title { get; set; }       // String
        public ChatThread CreatedAt { get; set; }   // DateTime
    }
}