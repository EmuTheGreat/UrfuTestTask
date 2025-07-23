using Dal.Models;
using Logic.Infrastructure.Results;
using Logic.Managers.Interfaces;

namespace Logic.Managers;

public class ProgramService : IProgramService
{
    public Task<IEnumerable<ProgramModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProgramModel?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}