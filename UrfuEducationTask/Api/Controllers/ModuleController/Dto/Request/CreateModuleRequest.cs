using Dal.Enums;

namespace Api.Controllers.ModuleController.Dto.Request;

public record CreateModuleRequest()
{
    public string Title { get; set; } = null!;
    public ModuleType Type { get; set; }
    public Guid ProgramId { get; set; }
}