using AuthenticationInterfaces.BindingModels;
using AuthenticationInterfaces.Interfaces;
using DatabaseContext;
using DepartmentWebCore.Models;
using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationProcess _serviceA;

        public AccountController(IAuthenticationProcess serviceA)
        {
            _serviceA = serviceA;
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
                await DepartmentUserManager.LoginAsync(model.Login, model.Password);

                await Authenticate();

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        private async Task Authenticate()
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, DepartmentUserManager.User.Id.ToString()),
                new Claim("username", DepartmentUserManager.User.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, DepartmentUserManager.Roles.Select(x => x.RoleName).FirstOrDefault())
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

        [HttpGet]
        public IActionResult UserPage()
        {
            return View();
        }

        [HttpPost]
        public async Task ChangePassword(ChangePasswordModel model)
        {
            if (model.NewPassword.Length < 6 || model.NewPassword.Length > 10)
            {
                throw new Exception("Длина пароля от 6 до 10 символов");
            }
            if (model.NewPassword != model.Confirmation)
            {
                throw new Exception("Новый пароль и подтверждение не совпадают");
            }

            var result = _serviceA.ChangePassword(new ChangePasswordBindingModels
            {
                Id = new Guid(User.Identity.Name),
                OldPassword = model.Password,
                NewPassword = model.NewPassword
            });

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Value);
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}