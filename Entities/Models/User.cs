using Contracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class User : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        // Unique: specified in model builder (in the related DbContext file)
        public string Username { get; set; }

        [Required]
        [MaxLength (20)]
        public string Password{ get; set; }

        [ForeignKey("Role")]
        public int RoleRefId { get; set; }
        public Role? Role { get; set; }
    }
}
