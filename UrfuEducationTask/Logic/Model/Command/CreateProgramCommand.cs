using System.ComponentModel.DataAnnotations;
using Dal.Enums;

namespace Logic.Model.Command;

/// <summary>
/// Команда для создания новой образовательной программы,
/// включая сразу список связанных модулей
/// </summary>
public class CreateProgramCommand
{
    [Required, MaxLength(200)]
    public string Title { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Status { get; set; } = null!;

    [Required, MaxLength(50)]
    public string Cypher { get; set; } = null!;

    [Required]
    public EducationLevel Level { get; set; }

    [Required]
    public EducationStandard Standard { get; set; }

    [Required]
    public Guid InstituteId { get; set; }

    [Required]
    public Guid HeadId { get; set; }

    [Required]
    public DateTime AccreditationTime { get; set; }

    /// <summary>
    /// Если нужно сразу привязать модули к программе,
    /// можно передать список их UUID
    /// </summary>
    public List<Guid> ModuleIds { get; set; } = new();
}