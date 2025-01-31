using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DAL
{
    [Table("roles")]
    public class Role
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        public ICollection<UserRole> UserRoles { get; set; }
    }
}