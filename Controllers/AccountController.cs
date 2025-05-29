using Microsoft.AspNetCore.Mvc;
using mvcApp.Models;
using mvcApp.Repository.Interfaces;

namespace mvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepo;

        public AccountController(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _accountRepo.Register(user);
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var user = _accountRepo.Login(email, password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserEmail", user.Email);
                return RedirectToAction("Index", "People");
            }

            ViewBag.Message = "Geçersiz e-posta veya şifre.";
            return View();
        }
    }
}
