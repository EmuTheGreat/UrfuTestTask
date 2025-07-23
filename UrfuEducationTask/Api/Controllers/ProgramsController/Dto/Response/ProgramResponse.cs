using Dal.Enums;

namespace Api.Controllers.ProgramsController.Dto.Response;

public record ProgramResponse
{
    public required Guid Uuid { get; set; }
    
    public required string Title { get; set; } = null!;
    
    public required string Status { get; set; } = null!;

    public required string Cypher { get; set; } = null!;

    public required EducationLevel Level { get; set; }

    public required EducationStandard Standard { get; set; }

    public required string InstituteTitle { get; set; }

    public required string HeadFullName { get; set; }

    public required DateTime AccreditationTime { get; set; }
}