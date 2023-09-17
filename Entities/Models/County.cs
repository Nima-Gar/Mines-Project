using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinesApi.Models
{
    public class County
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [ForeignKey("Province")]
        public int ProvinceRefId { get; set; }
        public Province? Province { get; set; }


    }
}
