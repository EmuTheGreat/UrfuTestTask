using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal.Models;

namespace Dal.Repositories.Interfaces
{
    public interface IHeadRepository
    {
        Task<List<Head>> GetAllAsync();
        Task<Head?> GetByIdAsync(Guid uuid);
        Task AddAsync(Head head);
        Task SaveChangesAsync();
    }
}