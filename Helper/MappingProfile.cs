
using AutoMapper;
using AskGemini.Dto;
using AskGemini.Models;

namespace AskGemini.Helper
{
    public class MappingProfile :Profile 
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }


    }
}
