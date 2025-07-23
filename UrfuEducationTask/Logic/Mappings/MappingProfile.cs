
using Dal.Models;
using Logic.Model;
using Microsoft.AspNetCore.Identity.Data;

namespace Api.Profile;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequest, UserLogicModel>();
        CreateMap<LoginRequest,    UserLogicModel>();
        //CreateMap<UserLogicModel, UserResponse>();
    }
}