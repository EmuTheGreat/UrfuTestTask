using AutoMapper;
using Dal.Models;
using Dal.Repositories.Interfaces;
using Logic.Infrastructure.Results;
using Logic.Managers.Interfaces;
using Logic.Model;
using Logic.Models.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Logic.Managers;

public class AccountManager : IAccountManager
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    
    public AccountManager(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IConfiguration configuration, IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _configuration = configuration;
        _mapper = mapper;
    }
    
    public async Task<OperationResult<string>> LoginAsync(UserLogicModel user)
    {
        var existing = await _userRepository.GetByPhone(user.Phone);
        
        var userModel = _mapper.Map<UserModel>(user);
        
        if (existing is null)
            return OperationResult<string>.Fail("Пользователя с таким номером не существует.");

        if (!_passwordHasher.VerifyPassword(user.Password, existing.Password))
            return OperationResult<string>.Fail("Неверный пароль.");

        var token = _jwtProvider.GenerateToken(
            userModel,
            _configuration["JwtOptions:Key"]!,
            _configuration["JwtOptions:Issuer"]!,
            int.Parse(_configuration["JwtOptions:ExpiresHours"]!)
        );
        return OperationResult<string>.Ok(token);
    }

    public async Task<OperationResult<string>> RegisterAsync(UserLogicModel user)
    {
        if (await _userRepository.GetByPhone(user.Phone) is not null)
            return OperationResult<string>.Fail("Пользователь с таким номером уже существует.");

        var userModel = _mapper.Map<UserModel>(user);
        
        userModel.Password = _passwordHasher.Generate(user.Password);
        await _userRepository.Create(userModel);

        var token = _jwtProvider.GenerateToken(
            userModel,
            _configuration["JwtOptions:Key"]!,
            _configuration["JwtOptions:Issuer"]!,
            int.Parse(_configuration["JwtOptions:ExpiresHours"]!)
        );

        return OperationResult<string>.Ok(token); // либо OperationResult<string>.Ok(token), если хотите вернуть его сразу
    }
}