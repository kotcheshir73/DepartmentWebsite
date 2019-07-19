using System.Collections.Generic;
using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebProcess
    {
        /// <summary>
		/// Получение списка комментариев одного уровня
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<List<WebProcessLevelCommentViewModel>> GetListLevelComment(CommentGetBindingModel model);

        /// <summary>
        /// Получение модели новости содержащуюю рекрсию листов коментариев
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<WebProcessEventWithCommentViewModel> GetEventWithComment(NewsGetBindingModel model);
    }
}