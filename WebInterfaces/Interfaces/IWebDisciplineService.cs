using System.Collections.Generic;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebDisciplineService
    {
        /// <summary>
        /// Получение дисциплины
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebDisciplineViewModel> GetDiscipline(WebDisciplineGetBindingModel model);

        /// <summary>
        /// Получить информацию по дисциплине и ее преподавателях
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebDisciplineContentInfo> GetDisciplineContentInfo(WebDisciplineContentInfoBindingModel model);

        /// <summary>
        /// Получение дисциплины
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebDisciplineViewModel> GetDisciplineName(WebDisciplineGetBindingModel model);

        /// <summary>
        /// Получение списка названий папок для дисциплины
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<List<WebDisciplineFolderNames>> GetDisciplineFolderNames(WebDisciplineFolderNamesBindingModel model);
    }
}