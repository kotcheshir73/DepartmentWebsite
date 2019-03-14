using DepartmentModel;
using TicketServiceInterfaces.BindingModels;
using TicketServiceInterfaces.ViewModels;

namespace TicketServiceInterfaces.Interfaces
{
    public interface IExaminationTemplateBlockService
    {
        /// <summary>
        /// Получение списка экзаменов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplatePageViewModel> GetExaminationTemplates(ExaminationTemplateGetBindingModel model);

        /// <summary>
        /// Получение списка блоков экзаменов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplateBlockPageViewModel> GetExaminationTemplateBlocks(ExaminationTemplateBlockGetBindingModel model);

        /// <summary>
        /// Получения блока экзамена
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ExaminationTemplateBlockViewModel> GetExaminationTemplateBlock(ExaminationTemplateBlockGetBindingModel model);

        /// <summary>
        /// Создание нового блока экзамена
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateExaminationTemplateBlock(ExaminationTemplateBlockSetBindingModel model);

        /// <summary>
        /// Изменение блока экзамена
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateExaminationTemplateBlock(ExaminationTemplateBlockSetBindingModel model);

        /// <summary>
        /// Удаление блока экзамена
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteExaminationTemplateBlock(ExaminationTemplateBlockGetBindingModel model);
    }
}