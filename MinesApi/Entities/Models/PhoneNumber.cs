using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MinesApi.Models
{

    public class PhoneNumber
    {


        [Column("NumberId")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Number { get; set; }


        [ForeignKey("PhoneNumType")]
        public int PhoneNumTypeRefId { get; set; }
        public PhoneNumType? PhoneNumType { get; set; }

        [ForeignKey("Mine")]
        public int MineRefId { get; set; }
        public Mine? Mine { get; set; }


    }
}
