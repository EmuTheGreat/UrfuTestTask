using System.ComponentModel.DataAnnotations;

namespace Dal.Models;

public class Institute
{
    [Key]
    public Guid Uuid { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = null!;

    // один институт — много программ
    public ICollection<ProgramModel> Programs { get; set; } = new List<ProgramModel>();
}
