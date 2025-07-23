using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal.Models;

namespace Dal.Repositories.Interfaces
{
    public interface IInstituteRepository
    {
        Task<List<Institute>> GetAllAsync();
        Task<Institute?> GetByIdAsync(Guid uuid);
    }
}