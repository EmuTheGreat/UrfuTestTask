using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.ProgramsController.Dto.Request;

public record ProgramUpdateRequest : ProgramCreateRequest
{
    [Required]
    public required Guid Uuid { get; init; }
}