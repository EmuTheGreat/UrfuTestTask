using Dal.Models;

namespace Dal.Repositories.Interfaces;

public interface IProgramRepository
{
    Task<List<ProgramModel>> GetAllAsync();
    Task<ProgramModel?> GetByIdAsync(Guid uuid);
    Task AddAsync(ProgramModel program);
    Task UpdateAsync(ProgramModel program);
    Task DeleteAsync(ProgramModel program);
    
    // методы для работы со связанными модулями
    /*Task<List<ModuleModel>> GetModulesByProgramIdAsync(Guid programId);
    Task AddModuleAsync(Guid programId, Guid moduleId);
    Task RemoveModuleAsync(Guid programId, Guid moduleId);*/
    Task SaveChangesAsync();
}