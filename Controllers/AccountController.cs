using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VaccinationCenter.Services;
using VaccinationCenter.ViewModels;

namespace VaccinationCenter.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICompteService _compteService;

        public AccountController(ICompteService compteService)
        {
            _compteService = compteService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var compte = await _compteService.AuthenticateAsync(model.Login, model.Password);
            if (compte == null)
            {
                ModelState.AddModelError("", "Login ou mot de passe incorrect");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, compte.Login),
                new(ClaimTypes.Role, compte.Role.ToString()),
                new("CompteId", compte.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            if (compte.Role == Models.Role.Admin)
                return RedirectToAction("Dashboard", "Admin");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (await _compteService.LoginExistsAsync(model.Login))
            {
                ModelState.AddModelError("Login", "Ce login est déjà utilisé");
                return View(model);
            }

            await _compteService.RegisterAsync(model.Login, model.Password);
            TempData["Success"] = "Compte créé avec succès ! Connectez-vous.";
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied() => View();
    }
}
