using Contracts;
using Microsoft.EntityFrameworkCore;
using Entities;
using MinesApi.Models;
using MinesApi.Models.ViewModels;
using System.Collections.Generic;
using Entities.Models.ViewModels;

namespace Repository
{
    public class MineRepository : RepositoryBase<Mine, MinesDbContext>, IMineRepository
    {
        private readonly MinesDbContext _context;
        private static IEnumerable<MineViewModel> _detailedMines;

        public MineRepository(MinesDbContext context) : base(context)
        {
            _context = context;
        }

        private async Task FillDetailedMinesList()
        {
            Console.WriteLine("fill method was called...");
            _detailedMines = await GetDetailedMines();
        }

        public async Task<IEnumerable<MineViewModel>> GetDetailedMines()
        {
            Console.WriteLine("Joining..");
            IQueryable<MineViewModel> detailedMines = (
                from m in _context.Mines
                join mt in _context.MineTypes on m.MineTypeRefId equals mt.Id
                join ot in _context.OwnershipTypes on m.OwnershipTypeRefId equals ot.Id
                join p in _context.Provinces on m.ProvinceRefId equals p.Id
                join c in _context.Counties on m.CountyRefId equals c.Id
                join s in _context.Statuses on m.StatusRefId equals s.Id
                select new MineViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    ComputerCode = m.ComputerCode,
                    OwnershipTypeRefId = m.OwnershipTypeRefId,
                    OwnershipType = ot.Title,
                    ProvinceRefId = m.ProvinceRefId,
                    Province = p.Title,
                    CountyRefId = m.CountyRefId,
                    County = c.Title,
                    Address = m.Address,
                    GeoghraphicPosition = m.GeoghraphicPosition,
                    InvestmentAmount = m.InvestmentAmount,
                    Degree = m.Degree,
                    Area = m.Area,
                    EmploymentCommitment = m.EmploymentCommitment,
                    MineTypeRefId = m.MineTypeRefId,
                    MineType = mt.Title,
                    StatusRefId = m.StatusRefId,
                    Status = s.Title,
                    PhoneNumbers = KeyValueToTNCouple(
                        (from ph in _context.PhoneNumbers
                         join pht in _context.PhoneNumTypes on ph.PhoneNumTypeRefId equals pht.Id
                         where ph.MineRefId == m.Id
                         select new { Type = pht.Title, ph.Number }
                        ).AsEnumerable().Select(q => new KeyValuePair<string, string>(q.Type, q.Number))
                    )
                }
            );
            return await detailedMines.ToListAsync();
        }


        private static List<TypeNumberCouple> KeyValueToTNCouple(IEnumerable<KeyValuePair<string, string>> TNPairsList)
        {
            List<TypeNumberCouple> typeNumberCouples = new List<TypeNumberCouple>();

            foreach (var typeNumber in TNPairsList)
                typeNumberCouples.Add(new TypeNumberCouple(typeNumber.Key, typeNumber.Value));

            return typeNumberCouples;
        }

        public async Task<IEnumerable<MineViewModel>> GetFilteredDetailedMines(Filters filters)
        {
            if(_detailedMines == null)
                await FillDetailedMinesList();

            var mines = _detailedMines;


            if (filters.Name != null && filters.Name != "")
                mines = mines.Where(detailedMine => detailedMine.Name.Contains(filters.Name));

            if (filters.ComputerCode != null && filters.ComputerCode != "")
                mines = mines.Where(detailedMine => detailedMine.ComputerCode.Contains(filters.ComputerCode));

            if (filters.OwnershipTypeRefId != null && filters.OwnershipTypeRefId != -1)
                mines = mines.Where(detailedMine => detailedMine.OwnershipTypeRefId == filters.OwnershipTypeRefId);

            if (filters.ProvinceRefId != null && filters.ProvinceRefId != -1)
                mines = mines.Where(detailedMine => detailedMine.ProvinceRefId == filters.ProvinceRefId);

            if (filters.CountyRefId != null && filters.CountyRefId != -1)
                mines = mines.Where(detailedMine => detailedMine.CountyRefId == filters.CountyRefId);

            if (filters.Address != null && filters.Address != "")
                mines = mines.Where(detailedMine => detailedMine.Address.Contains(filters.Address));

            if (filters.GeoghraphicPosition != null && filters.GeoghraphicPosition != "")
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

            if (filters.PhoneNumber != null && filters.PhoneNumber != "")
                mines = mines.Where(detailedMine => detailedMine.PhoneNumbers.Any(typeNumberCouple => typeNumberCouple.Number.Contains(filters.PhoneNumber)));

            return mines;
        }

    }
}