using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebAuthenticationService
    {
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebAuthenticationLoginViewModel> Authentication(WebAuthenticationLoginBindingModel model);

        ResultService ChangePassword(WebAuthenticationChangePassword model);
    }
}