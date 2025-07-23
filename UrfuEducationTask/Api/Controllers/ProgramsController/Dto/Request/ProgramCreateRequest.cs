using System.ComponentModel.DataAnnotations;
using Dal.Enums;

namespace Api.Controllers.ProgramsController.Dto.Request;

public record ProgramCreateRequest
{
    [Required, MaxLength(200)]
    public required string Title { get; set; } = null!;

    [Required, MaxLength(100)]
    public required string Status { get; set; } = null!;

    [Required, MaxLength(16)]
    public required string Cypher { get; set; } = null!;

    [Required]
    public required EducationLevel Level { get; set; }

    [Required]
    public required EducationStandard Standard { get; set; }

    [Required]
    public required Guid InstituteId { get; set; }

    [Required]
    public required Guid HeadId { get; set; }

    [Required]
    public required DateTime AccreditationTime { get; set; }
}