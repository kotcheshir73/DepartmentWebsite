using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace BaseInterfaces.Interfaces
{
    public interface IStudentOrderBlockService
    {
        /// <summary>
        /// Получение списка направлений
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model);

        /// <summary>
        /// Получение списка приказов по студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentOrderPageViewModel> GetStudentOrders(StudentOrderGetBindingModel model);

        /// <summary>
        /// Получение списка блоков приказа по студентам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentOrderBlockPageViewModel> GetStudentOrderBlocks(StudentOrderBlockGetBindingModel model);

        /// <summary>
        /// Получения блока приказа по студентам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentOrderBlockViewModel> GetStudentOrderBlock(StudentOrderBlockGetBindingModel model);

        /// <summary>
        /// Создание блока приказа по студентам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateStudentOrderBlock(StudentOrderBlockSetBindingModel model);

        /// <summary>
        /// Изменение блока приказа по студентам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateStudentOrderBlock(StudentOrderBlockSetBindingModel model);

        /// <summary>
        /// Удаление блока приказа по студентам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteStudentOrderBlock(StudentOrderBlockGetBindingModel model);
    }
}