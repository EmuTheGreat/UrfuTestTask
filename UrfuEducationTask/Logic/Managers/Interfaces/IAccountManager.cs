using Logic.Infrastructure.Results;
using Logic.Model;

namespace Logic.Managers.Interfaces;

public interface IAccountManager
{
    public Task<OperationResult<string>> LoginAsync(UserLogicModel user);
    public Task<OperationResult<string>> RegisterAsync(UserLogicModel user);
}