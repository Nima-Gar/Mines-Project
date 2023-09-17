namespace Entities.Models.ViewModels
{
    public class Filters
    {
        public string? Name { get; set; }

        public string? ComputerCode { get; set; }

        public int? OwnershipTypeRefId { get; set; }

        public int? ProvinceRefId { get; set; }

        public int? CountyRefId { get; set; }

        public string? Address { get; set; }

        public string? GeoghraphicPosition { get; set; }

        public int? InvestmentAmountUpperBound { get; set; }
        public int? InvestmentAmountLowerBound { get; set; }

        public short? DegreeUpperBound { get; set; }
        public short? DegreeLowerBound { get; set; }

        public int? AreaUpperBound { get; set; }
        public int? AreaLowerBound { get; set; }

        public bool? EmploymentCommitment { get; set; }

        public int? MineTypeRefId { get; set; }

        public int? StatusRefId { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
