using DepartmentModel;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceInterfaces.Interfaces
{
    public interface IExaminationTemplateBlockQuestionService
    {
        /// <summary>
        /// Получение списка блоков экзаменов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplateBlockPageViewModel> GetExaminationTemplateBlocks(ExaminationTemplateBlockGetBindingModel model);

        /// <summary>
        /// Получение списка вопросов блока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplateBlockQuestionPageViewModel> GetExaminationTemplateBlockQuestions(ExaminationTemplateBlockQuestionGetBindingModel model);

        /// <summary>
        /// Получения вопроса блока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplateBlockQuestionViewModel> GetExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionGetBindingModel model);

        /// <summary>
        /// Создание нового вопроса блока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionSetBindingModel model);

        /// <summary>
        /// Изменение вопроса блока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionSetBindingModel model);

        /// <summary>
        /// Удаление вопроса блока
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteExaminationTemplateBlockQuestion(ExaminationTemplateBlockQuestionGetBindingModel model);
    }
}