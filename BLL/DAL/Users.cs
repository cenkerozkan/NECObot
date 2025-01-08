using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BLL.DAL
{
    [Table("users")]
    public class Users
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // Use UUID as primary key
        
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty; // Username of the user
        
        [Required]
        [StringLength(50)]
        public string Password { get; set; } = string.Empty; // Password of the user
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for when the user was created
    }
}