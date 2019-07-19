using System.Collections.Generic;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebEducationDirectionService
    {
        ResultService<WebEducationDirectionPageViewModel> GetEducationDirections(WebEducationDirectionGetBindingModel model);

        ResultService<WebEducationDirectionViewModel> GetEducationDirection(WebEducationDirectionGetBindingModel model);

        /// <summary>
        /// Получение списка дисциплин на курсе
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<List<WebEducationDirectionDisciplineByCoursesViewModel>> GetDisciplinesByCourses(WebEducationDirectionDisciplineListInfoBindingModel model);
    }
}