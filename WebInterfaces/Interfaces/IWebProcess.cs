using System.Collections.Generic;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebProcess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        WebLoginViewModel Login(string login, string hash);

        /// <summary>
        /// Получение списка дисциплин на курсе
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<List<WebProcessDisciplineByCoursesViewModel>> GetDisciplinesByCourses(WebProcessDisciplineListInfoBindingModel model);

        /// <summary>
        /// Получить информацию по дисциплине и ее преподавателях
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebProcessDisciplineContentInfo> GetDisciplineContentInfo(WebProcessDisciplineContentInfoBindingModel model);

        /// <summary>
        /// Получение списка названий папок для дисциплины
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<List<WebProcessFolderNamesForDiscipline>> GetFolderNamesForDiscipline(WebProcessFolderNamesForDisciplineBindingModel model);

        /// <summary>
		/// Получение списка комментариев одного уровня
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<WebProcessLevelCommentViewModel>> GetListLevelComment(CommentGetBindingModel model);

        /// <summary>
		/// Создание папок для дисциплины
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		void CreateFolderDis(List<WebProcessFolderLoadSetBindingModel> model);

        /// <summary>
        /// Получение модели новости содержащуюю рекрсию листов коментариев
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebProcessEventWithCommentViewModel> GetEventWithComment(NewsGetBindingModel model);

        /// <summary>
        /// Получение 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebProcessDisciplineForDownloadViewModel> GetDisciplineForDownload(WebProcessDisciplineForDownloadGetBindingModel model);
    }
}