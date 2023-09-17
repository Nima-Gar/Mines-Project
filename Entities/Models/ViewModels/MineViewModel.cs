using Entities.Models.ViewModels;
using MinesApi.Models;

namespace MinesApi.Models.ViewModels
{
    public class MineViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string ComputerCode { get; set; }

        public int OwnershipTypeRefId { get; set; }
        public string? OwnershipType { get; set; }

        public int ProvinceRefId { get; set; }
        public string? Province { get; set; }

        public int CountyRefId { get; set; }
        public string? County { get; set; }

        public string Address { get; set; }

        public string GeoghraphicPosition { get; set; }

        public int InvestmentAmount { get; set; }

        public short Degree { get; set; }

        public int Area { get; set; }

        public bool EmploymentCommitment { get; set; }

        public int MineTypeRefId { get; set; }
        public string? MineType { get; set; }

        public int StatusRefId { get; set; }
        public string? Status { get; set; }

        public List<TypeNumberCouple>? PhoneNumbers { get; set; }
    }
}
