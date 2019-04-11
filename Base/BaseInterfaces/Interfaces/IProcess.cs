using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace BaseInterfaces.Interfaces
{
    public interface IProcess
    {
        /// <summary>
        /// Зачисление студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService EnrollmentStudents(StudentEnrollmentBindingModel model);

        /// <summary>
        /// Перевод студентов на следующий курс
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService TransferCourse(StudentTransferBindingModel model);

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
        ResultService TransferGroup(StudentTransferBindingModel model);

        /// <summary>
        /// Перевод студентов на другую специальность
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService FinishEducation(FinishEducationBindingModel model);
    }
}