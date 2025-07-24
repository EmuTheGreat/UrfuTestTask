using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories
{
    public class InstituteRepository : IInstituteRepository
    {
        private readonly UrfuDbContext _db;
        public InstituteRepository(UrfuDbContext db) => _db = db;

        public Task<List<Institute>> GetAllAsync() =>
            _db.Institutes.AsNoTracking().ToListAsync();

        public Task<Institute?> GetByIdAsync(Guid uuid) =>
            _db.Institutes.FindAsync(uuid).AsTask();

        public async Task AddAsync(Institute institute)
        {
            await _db.Institutes.AddAsync(institute);
        }

        public Task SaveChangesAsync() =>
            _db.SaveChangesAsync();
    }

}