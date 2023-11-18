using Contracts;
using Entities.Models.ViewModels;
using MediatR;

namespace Features.Mines.Queries
{
    public class GetFilteredDetailedMinesRequest : IRequest<IEnumerable<MineViewModel>>
    {
        public Filters Filters { get; set; }
    }

    internal class GetFilteredDetailedMinesRequestHandler : IRequestHandler<GetFilteredDetailedMinesRequest, IEnumerable<MineViewModel>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ICacheService _cacheService;

        public GetFilteredDetailedMinesRequestHandler(IRepositoryWrapper repository, ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<MineViewModel>> Handle(GetFilteredDetailedMinesRequest request, CancellationToken cancellationToken)
        {
            Filters filters = request.Filters;

            var mines = _cacheService.GetData<IEnumerable<MineViewModel>>("detailedMines");
            if (mines == null || !mines.Any())
            {
                mines = await _repository.MineRepo.GetDetailedMines();

                var expiaryTime = DateTimeOffset.Now.AddSeconds(30);
                _cacheService.SetData<IEnumerable<MineViewModel>>("detailedMines", mines, expiaryTime);
            }

            if (!string.IsNullOrEmpty(filters.Name))
                mines = mines.Where(detailedMine => detailedMine.Name.Contains(filters.Name));

            if (!string.IsNullOrEmpty(filters.ComputerCode))
                mines = mines.Where(detailedMine => detailedMine.ComputerCode.Contains(filters.ComputerCode));

            if (filters.OwnershipTypeRefId != null && filters.OwnershipTypeRefId != -1)
                mines = mines.Where(detailedMine => detailedMine.OwnershipTypeRefId == filters.OwnershipTypeRefId);

            if (filters.ProvinceRefId != null && filters.ProvinceRefId != -1)
                mines = mines.Where(detailedMine => detailedMine.ProvinceRefId == filters.ProvinceRefId);

            if (filters.CountyRefId != null && filters.CountyRefId != -1)
                mines = mines.Where(detailedMine => detailedMine.CountyRefId == filters.CountyRefId);

            if (!string.IsNullOrEmpty(filters.Address))
                mines = mines.Where(detailedMine => detailedMine.Address.Contains(filters.Address));

            if (!string.IsNullOrEmpty(filters.GeoghraphicPosition))
                mines = mines.Where(detailedMine => detailedMine.GeoghraphicPosition.Contains(filters.GeoghraphicPosition));

            if (filters.InvestmentAmountUpperBound != null && filters.InvestmentAmountLowerBound != null)
                mines = mines.Where(detailedMine =>
                    filters.InvestmentAmountLowerBound <= detailedMine.InvestmentAmount
                    && detailedMine.InvestmentAmount <= filters.InvestmentAmountUpperBound
             );
            else if (filters.InvestmentAmountUpperBound != null)
                mines = mines.Where(detailedMine => detailedMine.InvestmentAmount <= filters.InvestmentAmountUpperBound);
            else if (filters.InvestmentAmountLowerBound != null)
                mines = mines.Where(detailedMine =>
                    filters.InvestmentAmountLowerBound <= detailedMine.InvestmentAmount);

            if (filters.DegreeUpperBound != null && filters.DegreeLowerBound != null)
                mines = mines.Where(detailedMine =>
                    filters.DegreeLowerBound <= detailedMine.Degree
                    && detailedMine.Degree <= filters.DegreeUpperBound
             );
            else if (filters.DegreeUpperBound != null)
                mines = mines.Where(detailedMine => detailedMine.Degree <= filters.DegreeUpperBound);
            else if (filters.DegreeLowerBound != null)
                mines = mines.Where(detailedMine =>
                    filters.DegreeLowerBound <= detailedMine.Degree);

            if (filters.AreaUpperBound != null && filters.AreaLowerBound != null)
                mines = mines.Where(detailedMine =>
                    filters.AreaLowerBound <= detailedMine.Area
                    && detailedMine.Area <= filters.AreaUpperBound
             );
            else if (filters.AreaUpperBound != null)
                mines = mines.Where(detailedMine => detailedMine.Area <= filters.AreaUpperBound);
            else if (filters.AreaLowerBound != null)
                mines = mines.Where(detailedMine =>
                    filters.AreaLowerBound <= detailedMine.Area);


            if (filters.EmploymentCommitment != null)
                mines = mines.Where(detailedMine => detailedMine.EmploymentCommitment == filters.EmploymentCommitment);


            if (filters.MineTypeRefId != null && filters.MineTypeRefId != -1)
                mines = mines.Where(detailedMine => detailedMine.MineTypeRefId == filters.MineTypeRefId);


            if (filters.StatusRefId != null && filters.StatusRefId != -1)
                mines = mines.Where(detailedMine => detailedMine.StatusRefId == filters.StatusRefId);

            if (!string.IsNullOrEmpty(filters.PhoneNumber))
                mines = mines.Where(detailedMine => detailedMine.PhoneNumbers.Any(typeNumberCouple => typeNumberCouple.Number.Contains(filters.PhoneNumber)));

            return mines;
        }
    }
}