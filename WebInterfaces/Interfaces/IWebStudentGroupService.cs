using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebStudentGroupService
    {
        /// <summary>
        /// Получение списка групп
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebStudentGroupPageViewModel> GetStudentGroups(WebStudentGroupGetBindingModel model);
    }
}