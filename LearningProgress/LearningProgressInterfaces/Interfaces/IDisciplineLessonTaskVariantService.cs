using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.ViewModels;
using Tools;

namespace LearningProgressInterfaces.Interfaces
{
    public interface IDisciplineLessonTaskVariantService
    {
        /// <summary>
        /// Получение списка заданий
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonTaskPageViewModel> GetDisciplineLessonTasks(DisciplineLessonTaskGetBindingModel model);

        /// <summary>
        /// Получение списка заданий по варианту
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonTaskVariantPageViewModel> GetDisciplineLessonTaskVariants(DisciplineLessonTaskVariantGetBindingModel model);

        /// <summary>
        /// Получения задания по варианту
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineLessonTaskVariantViewModel> GetDisciplineLessonTaskVariant(DisciplineLessonTaskVariantGetBindingModel model);

        /// <summary>
        /// Создание нового задания по варианту
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateDisciplineLessonTaskVariant(DisciplineLessonTaskVariantRecordBindingModel model);

        /// <summary>
        /// Изменение задания по варианту
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDisciplineLessonTaskVariant(DisciplineLessonTaskVariantRecordBindingModel model);

        /// <summary>
        /// Удаление задания по варианту
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteDisciplineLessonTaskVariant(DisciplineLessonTaskVariantGetBindingModel model);
    }
}