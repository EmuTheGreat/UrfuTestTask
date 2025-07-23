using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal.Models;
using Logic.Infrastructure.Results;
using Logic.Model;
using Logic.Model.Command;

namespace Logic.Managers.Interfaces
{
    /// <summary>
    /// Операции над образовательными программами
    /// </summary>
    public interface IProgramService
    {
        Task<IEnumerable<ProgramLogicModel>> GetAllAsync();
        Task<ProgramLogicModel?> GetByIdAsync(Guid id);
        Task<OperationResult<ProgramLogicModel>> CreateAsync(CreateProgramCommand command);
        Task<OperationResult<ProgramLogicModel>> UpdateAsync(UpdateProgramCommand command);
        Task<OperationResult> DeleteAsync(Guid id);

        Task<IEnumerable<ProgramLogicModel>> GetModulesByProgramIdAsync(Guid programId);
        Task<OperationResult> AddModuleToProgramAsync(Guid programId, Guid moduleId);
        Task<OperationResult> RemoveModuleFromProgramAsync(Guid programId, Guid moduleId);
    }
}