using AutoMapper;
using CRUD.Models;
using DAL.Model;

namespace CRUD.Mapping
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player<int>, PlayersViewModel>().ReverseMap();
        }
    }
}
