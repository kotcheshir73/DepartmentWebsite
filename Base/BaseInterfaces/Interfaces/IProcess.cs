using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace BaseInterfaces.Interfaces
{
    public interface IProcess
    {
        /// <summary>
        /// Загрузка списка студентов из файла
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentPageViewModel> LoadStudentsFromFile(StudentLoadDocBindingModel model);

        /// <summary>
        /// Зачисление студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService EnrollmentStudents(StudentEnrollmentBindingModel model);

        /// <summary>
        /// Перевод студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService TransferStudents(StudentTransferBindingModel model);

        /// <summary>
        /// Отчисление студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeductionStudents(StudentDeductionBindingModel model);

        /// <summary>
        /// Уход студентов в академ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService ToAcademStudents(StudentAcademBindingModel model);

        /// <summary>
        /// Приход студентов из академа
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService FromAcademStudents(StudentAcademBindingModel model);

        /// <summary>
        /// Восстановление студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService RecoveryStudents(StudentRecoveryBindingModel model);

        /// <summary>
        /// Перевод студентов на другую специальность
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService TransferSpecStudents(StudentTransferBindingModel model);
    }
}