using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.ViewModels;
using Tools;

namespace ExaminationInterfaces.Interfaces
{
    public interface IExaminationTemplateTicketQuestionService
    {
        /// <summary>
        /// Получение списка билетов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplateTicketPageViewModel> GetExaminationTemplateTickets(ExaminationTemplateTicketGetBindingModel model);

        /// <summary>
        /// Получение списка вопросов билета
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplateTicketQuestionPageViewModel> GetExaminationTemplateTicketQuestions(ExaminationTemplateTicketQuestionGetBindingModel model);

        /// <summary>
        /// Получения вопроса
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplateTicketQuestionViewModel> GetExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionGetBindingModel model);

        /// <summary>
        /// Создание нового вопроса билета
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionSetBindingModel model);

        /// <summary>
        /// Изменение вопроса билета
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionSetBindingModel model);

        /// <summary>
        /// Удаление вопроса билета
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteExaminationTemplateTicketQuestion(ExaminationTemplateTicketQuestionGetBindingModel model);
    }
}