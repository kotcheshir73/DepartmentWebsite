using DatabaseContext;
using Enums;
using System;
using System.Linq;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace WebImplementations.Implementations
{
    public class WebAuthenticationService : IWebAuthenticationService
    {
        public ResultService<WebAuthenticationLoginViewModel> Authentication(WebAuthenticationLoginBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var user = context.DepartmentUsers.FirstOrDefault(u => u.UserName == model.Login && u.PasswordHash == model.Hash);

                    if (user == null)
                    {
                        throw new Exception("Введен неверный логин/пароль");
                    }
                    if (user.IsLocked)
                    {
                        throw new Exception("Пользователь заблокирован");
                    }

                    user.DateLastVisit = DateTime.Now;
                    context.SaveChanges();

                    var roles = context.DepartmentUserRoles.Where(x => x.UserId == user.Id).Select(x => x.Role.RoleName).OrderBy(x => x).ToList();

                    return ResultService<WebAuthenticationLoginViewModel>.Success(new WebAuthenticationLoginViewModel
                    {
                        UserId = user.Id.ToString(),
                        UserName = user.UserName,
                        UserRoles = roles
                    });
                }
            }
            catch (Exception ex)
            {
                return ResultService<WebAuthenticationLoginViewModel>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService ChangePassword(WebAuthenticationChangePassword model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var user = context.DepartmentUsers.FirstOrDefault(u => u.Id == model.Id && u.PasswordHash == model.OldPassword);

                    if (user == null)
                    {
                        throw new Exception("Введен неверный логин/пароль");
                    }
                    if (user.IsLocked)
                    {
                        throw new Exception("Пользователь заблокирован");
                    }

                    user.PasswordHash = model.NewPassword;
                    context.SaveChanges();

                    return ResultService.Success();
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}