using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Api.Controllers.AccountApiController.Dto.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _http;

        public AccountController(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient("api");
        }

        // GET /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resp = await _http.PostAsJsonAsync("/api/account/login", model);
            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
                return View(model);
            }

            // API поставил полезную токен-cookie, теперь залогинимся локально
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new System.Security.Claims.ClaimsPrincipal());

            return RedirectToAction("Index", "Home");
        }

        // GET /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resp = await _http.PostAsJsonAsync("/api/account/register", model);
            if (!resp.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Не удалось зарегистрироваться");
                return View(model);
            }

            return RedirectToAction("Login");
        }

        // POST /Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _http.PostAsync("/api/account/logout", null);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
