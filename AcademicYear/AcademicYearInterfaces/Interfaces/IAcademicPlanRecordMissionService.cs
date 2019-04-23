using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace AcademicYearInterfaces.Interfaces
{
    public interface IAcademicPlanRecordMissionService
    {
        /// <summary>
		/// Получение списка элементов записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicPlanRecordElementPageViewModel> GetAcademicPlanRecordElements(AcademicPlanRecordElementGetBindingModel model);

        /// <summary>
        /// Получение списка преподавателей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerPageViewModel> GetLecturers(LecturerGetBindingModel model);

        /// <summary>
		/// Получение списка элементов записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicPlanRecordMissionPageViewModel> GetAcademicPlanRecordMissions(AcademicPlanRecordMissionGetBindingModel model);

        /// <summary>
		/// Получение элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<AcademicPlanRecordMissionViewModel> GetAcademicPlanRecordMission(AcademicPlanRecordMissionGetBindingModel model);
        
        /// <summary>
		/// Создание новой элемента записи учебного плана
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateAcademicPlanRecordMission(AcademicPlanRecordMissionSetBindingModel model);

        /// <summary>
        /// Изменение элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateAcademicPlanRecordMission(AcademicPlanRecordMissionSetBindingModel model);

        /// <summary>
        /// Удаление элемента записи учебного плана
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteAcademicPlanRecordMission(AcademicPlanRecordMissionGetBindingModel model);
    }
}