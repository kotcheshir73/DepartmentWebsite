using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebEducationDirectionService
    {
        ResultService<WebEducationDirectionPageViewModel> GetEducationDirections(WebEducationDirectionGetBindingModel model);

        ResultService<WebEducationDirectionViewModel> GetEducationDirection(WebEducationDirectionGetBindingModel model);
    }
}