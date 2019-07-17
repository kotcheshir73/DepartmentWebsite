using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface INewsService
    {
        /// <summary>
        /// Получение списка новостей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<NewsPageViewModel> GetNewses(NewsGetBindingModel model);

        /// <summary>
        /// Получение новости
        /// </summary>
        /// <param name="model">Идентификатор новости</param>
        /// <returns></returns>
        ResultService<NewsViewModel> GetNews(NewsGetBindingModel model);

        /// <summary>
        /// Создание новости
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateNews(NewsSetBindingModel model);

        /// <summary>
        /// Изменение новости
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateNews(NewsSetBindingModel model);

        /// <summary>
        /// Удаление новости
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteNews(NewsGetBindingModel model);
    }
}