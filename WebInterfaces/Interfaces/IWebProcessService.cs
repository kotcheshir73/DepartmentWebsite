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
		ResultService<WebProcessLevelCommentPageViewModel> GetListLevelComment(CommentGetBindingModel model);

        /// <summary>
		/// Создание папок для дисциплины
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		void CreateFolderDis(List<WebProcessFolderLoadSetBindingModel> model);

        /// <summary>
		/// Получение 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<WebProcessDisciplineForDownloadViewModel> GetDisciplineForDownload(WebProcessDisciplineForDownloadGetBindingModel model);
    }
}
