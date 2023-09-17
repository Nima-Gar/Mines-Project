using Contracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinesApi.Models
{
    public class Mine : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ComputerCode { get; set; }

        [ForeignKey("OwnershipType")]
        public int OwnershipTypeRefId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public OwnershipType? OwnershipType { get; set; }

        [ForeignKey("Province")]
        public int ProvinceRefId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Province? Province { get; set; }

        [ForeignKey("County")]
        public int CountyRefId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public County? County { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string GeoghraphicPosition { get; set; }

        [Required]
        public int InvestmentAmount { get; set; }

        [Required]
        public short Degree { get; set; }

        [Required]
        public int Area { get; set; }

        [Required]
        public bool EmploymentCommitment { get; set; }

        [ForeignKey("MineType")]
        public int MineTypeRefId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public MineType? MineType { get; set; }

        [ForeignKey("Status")]
        public int StatusRefId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Status? Status { get; set; }

    }
}
