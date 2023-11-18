using Entities.Models.ViewModels;
using MinesApi.Models;

namespace Contracts
{
    public interface IMineRepository : IRepositoryBase<Mine>
    {
        Task<IEnumerable<MineViewModel>> GetDetailedMines();

        //Task<IEnumerable<MineViewModel>> GetFilteredDetailedMines(Filters filters);
    }
}