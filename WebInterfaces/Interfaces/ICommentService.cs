using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;
using Tools;
using System.Collections.Generic;

namespace WebInterfaces.Interfaces
{
    public interface ICommentService
    {
		/// <summary>
		/// Получение списка комментариев
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<CommentPageViewModel> GetComments(CommentGetBindingModel model);

        /// <summary>
        /// Получение списка ответов к комментарию
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<List<CommentViewModel>> GetAnswers(CommentGetBindingModel model);

        /// <summary>
        /// Получение комментария
        /// </summary>
        /// <param name="model">Идентификатор комментария</param>
        /// <returns></returns>
        ResultService<CommentViewModel> GetComment(CommentGetBindingModel model);

		/// <summary>
		/// Создание комментария
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateComment(CommentSetBindingModel model);

		/// <summary>
		/// Изменение комментария
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateComment(CommentSetBindingModel model);

        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteComment(CommentGetBindingModel model);
    }
}