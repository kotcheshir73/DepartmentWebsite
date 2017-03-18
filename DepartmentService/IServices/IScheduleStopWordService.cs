using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface IScheduleStopWordService
    {
		/// <summary>
		/// Получение списка стоп-слов
		/// </summary>
		/// <returns></returns>
		ResultService<List<ScheduleStopWordViewModel>> GetScheduleStopWords();

		/// <summary>
		/// Получения стоп-слова
		/// </summary>
		/// <param name="model">Идентификатор стоп-слова</param>
		/// <returns></returns>
		ResultService<ScheduleStopWordViewModel> GetScheduleStopWord(ScheduleStopWordGetBindingModel model);

        /// <summary>
        /// Создание нового стоп-слова
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateScheduleStopWord(ScheduleStopWordRecordBindingModel model);

        /// <summary>
        /// Изменение стоп-слова
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateScheduleStopWord(ScheduleStopWordRecordBindingModel model);

        /// <summary>
        /// Удаление стоп-слова
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteScheduleStopWord(ScheduleStopWordGetBindingModel model);
    }
}
