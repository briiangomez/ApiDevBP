using ApiDevBP.Entities;
using ApiDevBP.Models;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiDevBP.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap()
                .ForMember(x => x.Id, y => y.Ignore());
        }
    }
}
