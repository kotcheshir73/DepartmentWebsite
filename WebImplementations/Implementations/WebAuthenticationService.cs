using DatabaseContext;
using Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace WebImplementations.Implementations
{
    public class WebAuthenticationService : IWebAuthenticationService
    {
        public async Task<ResultService<WebAuthenticationLoginViewModel>> AuthenticationAsync(WebAuthenticationLoginBindingModel model)
        {
            try
            {
                await DepartmentUserManager.LoginAsync(model.Login, model.Password);

                return ResultService<WebAuthenticationLoginViewModel>.Success(new WebAuthenticationLoginViewModel
                {
                    UserId = DepartmentUserManager.User.Id.ToString(),
                    UserName = DepartmentUserManager.User.UserName,
                    UserRoles = DepartmentUserManager.Roles.Select(x => x.RoleName).ToList()
                });
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