using AutoMapper;
using Entities.Models.ViewModels;
using MinesApi.Models;
using Entities.Models;

namespace Features
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Mine, MineViewModel>();
            CreateMap<Mine, MineViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
//.EqualityComparison((entity, dto) => entity.Id == dto.Id);