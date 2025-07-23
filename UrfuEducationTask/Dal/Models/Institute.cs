using System.ComponentModel.DataAnnotations;

namespace Dal.Models;

public class Institute
{
    [Key]
    public Guid Uuid { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = null!;
    
    public List<Guid> Programs { get; set; } = new();
}
