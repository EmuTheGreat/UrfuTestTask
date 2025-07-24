using Dal.Models;

namespace Dal.Repositories.Interfaces;

public interface IModuleRepository
{
    Task<List<ModuleModel>> GetAllAsync();
    Task<List<ModuleModel>> GetByProgramIdAsync(Guid programId);
    Task<ModuleModel?> GetByIdAsync(Guid uuid);
    Task AddAsync(ModuleModel module);
    Task UpdateAsync(ModuleModel module);
    Task DeleteAsync(ModuleModel module);
    Task SaveChangesAsync();
}
