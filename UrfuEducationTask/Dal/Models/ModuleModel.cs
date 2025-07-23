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

    [Required]
    public EducationLevel Level { get; set; }

    [Required]
    public EducationStandard Standard { get; set; }

    // many-to-many с программами
    public ICollection<ProgramModel> Programs { get; set; } = new List<ProgramModel>();
}