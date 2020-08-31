using System.Threading.Tasks;
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
        Task<ResultService<WebAuthenticationLoginViewModel>> AuthenticationAsync(WebAuthenticationLoginBindingModel model);

        ResultService ChangePassword(WebAuthenticationChangePassword model);
    }
}