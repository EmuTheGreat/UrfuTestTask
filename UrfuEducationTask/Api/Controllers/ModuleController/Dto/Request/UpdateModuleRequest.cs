using Dal.Enums;

namespace Api.Controllers.ModuleController.Dto.Request;

public record UpdateModuleRequest()
{
    public string Title { get; set; } = null!;
    public ModuleType Type { get; set; }
}