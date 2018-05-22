using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface ISoftwareRecordService
    {
        /// <summary>
        /// Получение списка материально-технических ценностей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<MaterialTechnicalValuePageViewModel> GetMaterialTechnicalValues(MaterialTechnicalValueGetBindingModel model);

        /// <summary>
        /// Получение списка ПО
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<SoftwareRecordPageViewModel> GetSoftwareRecords(SoftwareRecordGetBindingModel model);

        /// <summary>
        /// Получения ПО
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<SoftwareRecordViewModel> GetSoftwareRecord(SoftwareRecordGetBindingModel model);

        /// <summary>
        /// Создание ПО
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateSoftwareRecord(SoftwareRecordSetBindingModel model);

        /// <summary>
        /// Изменение ПО
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateSoftwareRecord(SoftwareRecordSetBindingModel model);

        /// <summary>
        /// Удаление ПО
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteSoftwareRecord(SoftwareRecordSetBindingModel model);
    }
}
