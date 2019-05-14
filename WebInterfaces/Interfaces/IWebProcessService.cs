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
    }
}
