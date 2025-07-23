using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories
{
    public class HeadRepository : IHeadRepository
    {
        private readonly UrfuDbContext _db;
        public HeadRepository(UrfuDbContext db) => _db = db;

        public Task<List<Head>> GetAllAsync() =>
            _db.Heads.AsNoTracking().ToListAsync();

        public Task<Head?> GetByIdAsync(Guid uuid) =>
            _db.Heads.FindAsync(uuid).AsTask();
    }
}