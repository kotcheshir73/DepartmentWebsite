using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationImplementations.Implementations;
using AuthenticationInterfaces.Interfaces;
using AuthenticationInterfaces.ViewModels;
using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Tools;

namespace DepartmentWebCore.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationProcess _serviceA;

        public AccountController(IAuthenticationProcess serviceA )
        {
            _serviceA = serviceA;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var loginModel = _serviceA.Login(model.Login, DepartmentUserManager.GetPasswordHash(model.Password));
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

        private async Task Authenticate(LoginViewModel user)
        {
            string role = "";
            if(user.UserRoles.FirstOrDefault(x => x == "Студент") != null)
            {
                role = "Студент";
            }
            else if(user.UserRoles.FirstOrDefault(x => x == "Преподаватель") != null)
            {
                role = "Преподаватель";
            }
            else
            {
                role = "Неопределенный";
            }
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)

            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}