using Dal.Models;
using Logic.Infrastructure.Results;

namespace Logic.Managers.Interfaces
{
    /// <summary>
    /// Сервис для работы с образовательными программами
    /// </summary>
    public interface IProgramService
    {
        /// <summary>
        /// Получить все программы
        /// </summary>
        Task<IEnumerable<ProgramModel>> GetAllAsync();

        /// <summary>
        /// Получить программу по идентификатору
        /// </summary>
        Task<ProgramModel?> GetByIdAsync(Guid id);

        /// <summary>
        /// Удалить программу по идентификатору
        /// </summary>
        Task<OperationResult> DeleteAsync(Guid id);
    }
}