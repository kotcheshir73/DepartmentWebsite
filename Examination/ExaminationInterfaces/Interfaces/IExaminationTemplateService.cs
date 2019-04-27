using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface IExaminationTemplateService
    {
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
        /// Получение списка экзаменов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplatePageViewModel> GetExaminationTemplates(ExaminationTemplateGetBindingModel model);

        /// <summary>
        /// Получения экзамена
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplateViewModel> GetExaminationTemplate(ExaminationTemplateGetBindingModel model);

        /// <summary>
        /// Создание нового экзамена
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateExaminationTemplate(ExaminationTemplateSetBindingModel model);

        /// <summary>
        /// Изменение экзамена
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateExaminationTemplate(ExaminationTemplateSetBindingModel model);

        /// <summary>
        /// Удаление экзамена
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteExaminationTemplate(ExaminationTemplateGetBindingModel model);
    }
}