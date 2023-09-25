using Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class User : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        // Unique: specified in model builder (in the related DbContext file)
        public string? Username { get; set; }

        [Required]
        [MaxLength (20)]
        public string? Password{ get; set; }

        [Required]
        [MaxLength(20)]
        public string? GivenName { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Surname { get; set; }

        [ForeignKey("Role")]
        public int RoleRefId { get; set; }
        public Role? Role { get; set; }
    }
}
