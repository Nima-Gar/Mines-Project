using AutoMapper;
using MinesApi.Models.ViewModels;
using MinesApi.Models;
using Entities.Models;
using Entities.Models.ViewModels;

namespace MinesApi.Common
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Mine, MineViewModel>();
            CreateMap<Mine, MineViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
//.EqualityComparison((entity, dto) => entity.Id == dto.Id);