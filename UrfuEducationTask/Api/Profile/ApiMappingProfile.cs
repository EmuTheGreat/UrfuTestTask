using Api.Controllers.AccountApiController.Request;
using Dal.Models;
using Logic.Model;

namespace Api.Mappings
{
    public class ApiMappingProfile : AutoMapper.Profile
    {
        public ApiMappingProfile()
        {
            // Request → LogicModel
            CreateMap<RegisterRequest, UserLogicModel>();
            CreateMap<LoginRequest, UserLogicModel>();
            CreateMap<UserLogicModel, UserModel>();
            CreateMap<UserModel, UserLogicModel>();

            // LogicModel → Response
            //CreateMap<UserLogicModel, UserResponse>();
        }
    }
}