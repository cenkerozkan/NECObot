using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DAL
{
    [Table("message_categories")]
    public class MessageCategory
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        
        public Guid AcceptedMessageId { get; set; }
        public AcceptedMessage AcceptedMessage { get; set; }
    }
} 