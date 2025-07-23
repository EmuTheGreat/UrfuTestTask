using Dal.Models;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UrfuDbContext _dbContext;

    public UserRepository(UrfuDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(UserModel user)
    {
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserModel> GetByPhone(string phone)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Phone == phone);
    }
}