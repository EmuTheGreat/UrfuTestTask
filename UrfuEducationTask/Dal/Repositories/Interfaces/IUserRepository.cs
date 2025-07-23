using Dal.Models;

namespace Dal.Repositories.Interfaces;

public interface IUserRepository
{
    public Task Create(UserModel user);
    public Task<UserModel> GetByPhone(string phone);
}