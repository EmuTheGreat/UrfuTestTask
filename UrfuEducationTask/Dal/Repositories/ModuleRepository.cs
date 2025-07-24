using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public class ModuleRepository : IModuleRepository
{
    private readonly UrfuDbContext _db;
    public ModuleRepository(UrfuDbContext db) => _db = db;

    public Task<List<ModuleModel>> GetAllAsync() =>
        _db.Modules
            .Include(m => m.Program)
            .AsNoTracking()
            .ToListAsync();

    public Task<List<ModuleModel>> GetByProgramIdAsync(Guid programId) =>
        _db.Modules
            .Where(m => m.ProgramId == programId)
            .Include(m => m.Program)
            .AsNoTracking()
            .ToListAsync();

    public Task<ModuleModel?> GetByIdAsync(Guid uuid) =>
        _db.Modules.FindAsync(uuid).AsTask();

    public async Task AddAsync(ModuleModel module) =>
        await _db.Modules.AddAsync(module);

    public Task UpdateAsync(ModuleModel module)
    {
        _db.Modules.Update(module);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(ModuleModel module)
    {
        _db.Modules.Remove(module);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync() => _db.SaveChangesAsync();
}
