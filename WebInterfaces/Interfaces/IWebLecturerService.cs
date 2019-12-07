using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebLecturerService
    {
        /// <summary>
        /// Получение списка преподавателей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebLecturerPageViewModel> GetLecturers(WebLecturerGetBindingModel model);

        /// <summary>
        /// Получения преподавателя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebLecturerViewModel> GetLecturer(WebLecturerGetBindingModel model);
    }
}