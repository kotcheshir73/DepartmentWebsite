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
    public interface IDisciplineLessonRecordService
    {
        /// <summary>
        /// Получение списка занятий
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonPageViewModel> GetDisciplineLessons(DisciplineLessonGetBindingModel model);

        /// <summary>
        /// Получение списка записей о занятии
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonRecordPageViewModel> GetDisciplineLessonRecords(DisciplineLessonRecordGetBindingModel model);

        /// <summary>
        /// Получения записи о занятии
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonRecordViewModel> GetDisciplineLessonRecord(DisciplineLessonRecordGetBindingModel model);

        /// <summary>
        /// Создание записи о занятии
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateDisciplineLessonRecord(DisciplineLessonRecordSetBindingModel model);

        /// <summary>
        /// Изменение записи о занятии
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDisciplineLessonRecord(DisciplineLessonRecordSetBindingModel model);

        /// <summary>
        /// Удаление записи о занятии
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteDisciplineLessonRecord(DisciplineLessonRecordGetBindingModel model);
    }
}
