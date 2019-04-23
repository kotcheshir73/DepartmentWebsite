using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.ViewModels;
using Tools;

namespace LearningProgressInterfaces.Interfaces
{
    public interface IDisciplineLessonService
    {
        /// <summary>
        /// Получение списка учебных годов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

        /// <summary>
        /// Получение списка дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplinePageViewModel> GetDisciplines(DisciplineGetBindingModel model);

        /// <summary>
        /// Получение списка направлений
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<EducationDirectionPageViewModel> GetEducationDirections(EducationDirectionGetBindingModel model);

        /// <summary>
        /// Получение списка норм времени
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<TimeNormPageViewModel> GetTimeNorms(TimeNormGetBindingModel model);

        /// <summary>
        /// Получение списка занятий
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonPageViewModel> GetDisciplineLessons(DisciplineLessonGetBindingModel model);
        
        /// <summary>
        /// Получения занятия
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonViewModel> GetDisciplineLesson(DisciplineLessonGetBindingModel model);

        /// <summary>
        /// Создание нового занятия
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateDisciplineLesson(DisciplineLessonRecordBindingModel model);

        /// <summary>
        /// Изменение занятия
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDisciplineLesson(DisciplineLessonRecordBindingModel model);

        /// <summary>
        /// Удаление занятия
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteDisciplineLesson(DisciplineLessonGetBindingModel model);
    }
}