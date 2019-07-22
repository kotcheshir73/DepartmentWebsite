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
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IWebAuthenticationService _serviceWA;

        public AccountController(IWebAuthenticationService serviceWA)
        {
            _serviceWA = serviceWA;
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
                    var loginModel = _serviceWA.Authentication(new WebAuthenticationLoginBindingModel
                    {
                        Login = model.Login,
                        Hash = DepartmentUserManager.GetPasswordHash(model.Password)
                    });

                    if (!loginModel.Succeeded)
                    {

                    }

                    await Authenticate(loginModel.Result);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)//TODO: если неверные данны выдавать ошибку
                {
                    ModelState.AddModelError("fdhhfdhdfh", ex.Message);
                }
            }
            return View(model);
        }

        private async Task Authenticate(WebAuthenticationLoginViewModel user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserId),
                new Claim("username", user.UserName),
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

            var result = _serviceWA.ChangePassword(new WebAuthenticationChangePassword
            {
                Id = new Guid(User.Identity.Name),
                OldPassword = DepartmentUserManager.GetPasswordHash(model.Password),
                NewPassword = DepartmentUserManager.GetPasswordHash(model.NewPassword)
            });

            if(!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Value);
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}