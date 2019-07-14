using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebContingentService
    {
        ResultService<WebContingentPageViewModel> GetCourseByContingents(WebContingentGetBindingModel model);
    }
}