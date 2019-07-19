using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tools;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IWebProcess _process;

        public AccountController(IWebProcess process)
        {
            _process = process;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var loginModel = _process.Login(model.Login, DepartmentUserManager.GetPasswordHash(model.Password));
                    await Authenticate(loginModel);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)//TODO: если неверные данны выдавать ошибку
                {                    
                    ModelState.AddModelError("fdhhfdhdfh", ex.Message);
                }
            }
            return View(model);
        }

        private async Task Authenticate(WebLoginViewModel user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserId),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserRoles.FirstOrDefault())
            };

            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpPost]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}