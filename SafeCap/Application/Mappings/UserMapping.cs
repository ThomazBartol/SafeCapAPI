using AutoMapper;
using SafeCap.Domain.Entities;
using SafeCap.Application.DTOs.Response;

namespace SafeCap.Application.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping() 
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserResponse, User>();
        }
    }
}
