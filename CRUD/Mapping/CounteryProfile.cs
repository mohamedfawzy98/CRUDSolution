using AutoMapper;
using CRUD.Models;
using DAL.Model;

namespace CRUD.Mapping
{
    public class CounteryProfile : Profile
    {
        public CounteryProfile()
        {
            CreateMap<CounteryViewModel, Countery<int>>().ReverseMap();
        }
    }
}
