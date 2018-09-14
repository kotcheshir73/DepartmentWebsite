using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.BindingModels.StandartBindingModels.LearningProgress;
using DepartmentService.ViewModels;
using DepartmentService.ViewModels.StandartViewModels.LearningProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.IServices.StandartInterfaces.LearningProgress
{
    public interface IDisciplineLessonStudentRecordService
    {
        /// <summary>
        /// Получение списка записей о занятии
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonRecordPageViewModel> GetDisciplineLessonRecords(DisciplineLessonRecordGetBindingModel model);

        /// <summary>
        /// Получение списка студентов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<StudentPageViewModel> GetStudents(StudentGetBindingModel model);

        /// <summary>
        /// Получение списка связок занятие-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonStudentRecordPageViewModel> GetDisciplineLessonStudentRecords(DisciplineLessonStudentRecordGetBindingModel model);

        /// <summary>
        /// Получения связки занятие-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonStudentRecordViewModel> GetDisciplineLessonStudentRecord(DisciplineLessonStudentRecordGetBindingModel model);

        /// <summary>
        /// Создание новой связки занятие-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateDisciplineLessonStudentRecord(DisciplineLessonStudentRecordSetBindingModel model);

        /// <summary>
        /// Изменение связки занятие-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDisciplineLessonStudentRecord(DisciplineLessonStudentRecordSetBindingModel model);

        /// <summary>
        /// Удаление связки занятие-студент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteDisciplineLessonStudentRecord(DisciplineLessonStudentRecordGetBindingModel model);
    }
}
