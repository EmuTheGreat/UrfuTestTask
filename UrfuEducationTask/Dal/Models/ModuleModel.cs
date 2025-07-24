using System.ComponentModel.DataAnnotations;
using Dal.Enums;

namespace Dal.Models;

public class ModuleModel
{
    [Key]
    public Guid Uuid { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = null!;

    [Required]
    public ModuleType Type { get; set; }

    // FK на программу
    [Required]
    public Guid ProgramId { get; set; }

    public ProgramModel Program { get; set; } = null!;
}