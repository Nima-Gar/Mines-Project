using Contracts;
using Microsoft.EntityFrameworkCore;
using Entities;
using MinesApi.Models;
using Entities.Models.ViewModels;
using System.Collections.Generic;

namespace Repository
{
    public class MineRepository : RepositoryBase<Mine, MinesDbContext>, IMineRepository
    {
        private readonly MinesDbContext _context;
        //private static IEnumerable<MineViewModel> _detailedMines;

        public MineRepository(MinesDbContext context) : base(context)
        {
            _context = context;
        }

        //private async Task FillDetailedMinesList()
        //{
        //    Console.WriteLine("fill method was called...");
        //    _detailedMines = await GetDetailedMines();
        //}

        // then in function: 
        //if(_detailedMines == null)
        //    await FillDetailedMinesList();

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
            List<TypeNumberCouple> typeNumberCouples = new();

            foreach (var typeNumber in TNPairsList)
                typeNumberCouples.Add(new TypeNumberCouple(typeNumber.Key, typeNumber.Value));

            return typeNumberCouples;
        }

    }
}