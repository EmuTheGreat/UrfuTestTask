using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public class ProgramRepository : IProgramRepository
{
    private readonly UrfuDbContext _db;
    public ProgramRepository(UrfuDbContext db) => _db = db;

    public Task<List<ProgramModel>> GetAllAsync() =>
        _db.Programs.AsNoTracking().ToListAsync();

    public Task<ProgramModel?> GetByIdAsync(Guid uuid) =>
        _db.Programs.FindAsync(uuid).AsTask();

    public Task AddAsync(ProgramModel program) =>
        _db.Programs.AddAsync(program).AsTask();

    public Task UpdateAsync(ProgramModel program)
    {
        _db.Programs.Update(program);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(ProgramModel program)
    {
        _db.Programs.Remove(program);
        return Task.CompletedTask;
    }

    /*public async Task<List<ModuleModel>> GetModulesByProgramIdAsync(Guid programId)
    {
        var prog = await _db.Programs.FindAsync(programId);
        if (prog == null) return new List<ModuleModel>();
        // предположим, вы храните ModuleIds как JSON List<Guid>
        return await _db.Modules
            .Where(m => prog.ModuleIds.Contains(m.Uuid))
            .AsNoTracking()
            .ToListAsync();
    }*/

    /*public async Task AddModuleAsync(Guid programId, Guid moduleId)
    {
        var prog = await _db.Programs.FindAsync(programId);
        if (prog == null) throw new InvalidOperationException("Program not found");
        if (!prog.ModuleIds.Contains(moduleId))
            prog.ModuleIds.Add(moduleId);
    }

    public async Task RemoveModuleAsync(Guid programId, Guid moduleId)
    {
        var prog = await _db.Programs.FindAsync(programId);
        if (prog == null) throw new InvalidOperationException("Program not found");
        prog.ModuleIds.RemoveAll(id => id == moduleId);
    }*/

    public Task SaveChangesAsync() => _db.SaveChangesAsync();
}