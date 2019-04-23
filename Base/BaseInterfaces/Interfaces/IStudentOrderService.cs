using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace BaseInterfaces.Interfaces
{
    public interface IStudentOrderService
    {
        /// <summary>
        /// Получение списка приказов по студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentOrderPageViewModel> GetStudentOrders(StudentOrderGetBindingModel model);

        /// <summary>
        /// Получения приказа по студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentOrderViewModel> GetStudentOrder(StudentOrderGetBindingModel model);

        /// <summary>
        /// Создание приказа по студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStudentOrder(StudentOrderSetBindingModel model);

        /// <summary>
        /// Изменение приказа по студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStudentOrder(StudentOrderSetBindingModel model);

        /// <summary>
        /// Удаление приказа по студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStudentOrder(StudentOrderGetBindingModel model);
    }
}