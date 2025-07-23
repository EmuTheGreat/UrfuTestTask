using System.ComponentModel.DataAnnotations;

namespace Dal.Models;

public class Head
{
    [Key]
    public Guid Uuid { get; set; }

    [Required, MaxLength(200)]
    public string FullName { get; set; } = null!;

    // один ответственный — много программ
    public ICollection<ProgramModel> Programs { get; set; } = new List<ProgramModel>();
}