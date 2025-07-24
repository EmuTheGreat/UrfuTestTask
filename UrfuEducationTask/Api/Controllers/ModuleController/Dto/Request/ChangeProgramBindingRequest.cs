namespace Api.Controllers.ModuleController.Dto;

public record ChangeProgramBindingRequest
{
    public Guid ModuleId { get; set; }
    public Guid ProgramId { get; set; }
}