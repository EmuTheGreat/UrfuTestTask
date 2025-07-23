using System.Security.Claims;
using Api.Controllers.AccountApiController.Request;
using AutoMapper;
using Logic.Infrastructure.Results;
using Logic.Managers.Interfaces;
using Logic.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AccountApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountManager _accountManager;

        public AccountController(IMapper mapper, IAccountManager accountManager)
        {
            _mapper = mapper;
            _accountManager = accountManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var userModel = _mapper.Map<UserLogicModel>(registerRequest);
            var result = await _accountManager.RegisterAsync(userModel);

            if (!result.Success)
                return BadRequest(new { Message = result.Error });

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var userModel = _mapper.Map<UserLogicModel>(loginRequest);
            var result = await _accountManager.LoginAsync(userModel);

            if (!result.Success)
                return BadRequest(new { Message = result.Error });

            // ставим токен в HTTP-only cookie
            Response.Cookies.Append(
                "tasty-cookies",
                result.Data!,
                new CookieOptions
                {
                    HttpOnly  = true,
                    Secure    = true,
                    SameSite  = SameSiteMode.Strict
                });

            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("tasty-cookies");
            return Ok();
        }
    }
}
