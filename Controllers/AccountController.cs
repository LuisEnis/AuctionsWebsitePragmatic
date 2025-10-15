using AuctionsWebsitePragmatic.Services.Interfaces;
using AuctionsWebsitePragmatic.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuctionsWebsitePragmatic.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //return View(model);
                return BadRequest(ModelState);
            }

            var (success, error) = await _authService.RegisterAsync(model);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, error);
                //return View(model);
                return BadRequest(error);
            }

            //return RedirectToAction(nameof(Login));
            return Ok(new { message = "User registered successfully", userId = model.Username });
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                //return View(model);
                return BadRequest(ModelState);
            }

            var (success, error, user) = await _authService.ValidateLoginAsync(model);
            if (!success || user == null)
            {
                ModelState.AddModelError(string.Empty, error ?? "Invalid credentials.");
                //return View(model);
                return BadRequest(error);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            //return RedirectToAction("Index", "Home");
            return Ok(new { message = "User logged in successfully", userId = model.Email });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //return RedirectToAction(nameof(Login));
            return Ok(new { message = "User logged out successfully"});
        }
    }
}
