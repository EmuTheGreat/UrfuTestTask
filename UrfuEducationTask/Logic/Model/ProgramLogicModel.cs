using Dal.Enums;

namespace Logic.Model;

public class ProgramLogicModel
{
    public Guid Uuid { get; set; }

    public string Title { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Cypher { get; set; } = null!;

    public EducationLevel Level { get; set; }

    public EducationStandard Standard { get; set; }

    public Guid InstituteId { get; set; }

    public string InstituteTitle { get; set; } = null!;

    public Guid HeadId { get; set; }

    public string HeadFullName { get; set; } = null!;

    public DateTime AccreditationTime { get; set; }
    
    public List<Guid> ModuleIds { get; set; } = new();
}