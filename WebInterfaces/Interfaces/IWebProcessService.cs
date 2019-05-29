using System;
using System.Collections.Generic;
using System.Text;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebProcessService
    {
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
        ResultService<WebProcessEventWithCommentViewModel> GetEventWithComment(EventGetBindingModel model);

        /// <summary>
        /// Получение 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebProcessDisciplineForDownloadViewModel> GetDisciplineForDownload(WebProcessDisciplineForDownloadGetBindingModel model);
    }
}
