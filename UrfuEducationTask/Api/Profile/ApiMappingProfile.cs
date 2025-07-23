using Api.Controllers.AccountApiController.Dto.Request;
using Api.Controllers.ProgramsController.Dto.Request;
using Api.Controllers.ProgramsController.Dto.Response;
using Dal.Models;
using Logic.Model;
using Logic.Model.Command;

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
            
            CreateMap<ProgramCreateRequest, CreateProgramCommand>();
            CreateMap<ProgramUpdateRequest, UpdateProgramCommand>();

            CreateMap<CreateProgramCommand, ProgramModel>();
            
            CreateMap<ProgramLogicModel, ProgramResponse>();
            
            
            
        }
    }
}