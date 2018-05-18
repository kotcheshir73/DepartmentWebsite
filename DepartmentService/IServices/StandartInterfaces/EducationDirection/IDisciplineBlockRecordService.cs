using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IDisciplineBlockRecordService
    {
        /// <summary>
        /// Получение списка блоков дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineBlockPageViewModel> GetDisciplineBlocks(DisciplineBlockGetBindingModel model);

        /// <summary>
        /// Получение списка учебных годов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<AcademicYearPageViewModel> GetAcademicYears(AcademicYearGetBindingModel model);

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
        /// Получение списка записей блока дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineBlockRecordPageViewModel> GetDisciplineBlockRecords(DisciplineBlockRecordGetBindingModel model);

        /// <summary>
        /// Получения записи блока дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<DisciplineBlockRecordViewModel> GetDisciplineBlockRecord(DisciplineBlockRecordGetBindingModel model);

        /// <summary>
        /// Создание новой записи блока дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateDisciplineBlockRecord(DisciplineBlockRecordSetBindingModel model);

        /// <summary>
        /// Изменение записи блока дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateDisciplineBlockRecord(DisciplineBlockRecordSetBindingModel model);

        /// <summary>
        /// Удаление записи блока дисциплин
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteDisciplineBlockRecord(DisciplineBlockRecordGetBindingModel model);
    }
}
