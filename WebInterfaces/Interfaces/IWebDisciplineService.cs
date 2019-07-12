using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebDisciplineService
    {
        /// <summary>
        /// Получение списка дисциплин на год по направлению
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebDisciplinePageViewModel> GetDisciplines(WebDisciplineGetBindingModel model);

        /// <summary>
        /// Получение дисциплины
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebDisciplineViewModel> GetDiscipline(WebDisciplineGetBindingModel model);

        /// <summary>
        /// Изменение дисциплины
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDiscipline(WebDisciplineSetBindingModel model);
    }
}