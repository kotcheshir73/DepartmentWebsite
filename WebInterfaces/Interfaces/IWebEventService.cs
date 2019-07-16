using Tools;
using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;

namespace WebInterfaces.Interfaces
{
    public interface IWebEventService
    {
        /// <summary>
        /// Получение списка новостей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<EventPageViewModel> GetEvents(EventGetBindingModel model);

        /// <summary>
        /// Получение новости
        /// </summary>
        /// <param name="model">Идентификатор новости</param>
        /// <returns></returns>
        ResultService<EventViewModel> GetEvent(EventGetBindingModel model);

        /// <summary>
        /// Создание новости
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateEvent(EventSetBindingModel model);

        /// <summary>
        /// Изменение новости
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateEvent(EventUpdateBindingModel model);

        /// <summary>
        /// Удаление новости
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteEvent(EventGetBindingModel model);
    }
}