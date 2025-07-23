using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Dal.Enums;

namespace Dal.Models;

public class ProgramModel
{
    [Key]
    public Guid Uuid { get; set; }

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
    public Institute Institute { get; set; } = null!;

    [Required]
    public Guid HeadId { get; set; }
    public Head Head { get; set; } = null!;

    [Required]
    public DateTime AccreditationTime { get; set; }

    // many-to-many с модулями
    public ICollection<ModuleModel> Modules { get; set; } = new List<ModuleModel>();
}