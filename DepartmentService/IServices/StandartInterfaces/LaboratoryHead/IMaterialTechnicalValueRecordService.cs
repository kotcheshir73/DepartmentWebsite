using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IMaterialTechnicalValueRecordService
    {
        /// <summary>
        /// Получение списка материально-технических ценностей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<MaterialTechnicalValuePageViewModel> GetMaterialTechnicalValues(MaterialTechnicalValueGetBindingModel model);

        /// <summary>
        /// Получение списка групп описаний материально-технических ценностей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<MaterialTechnicalValueGroupPageViewModel> GetMaterialTechnicalValueGroups(MaterialTechnicalValueGroupGetBindingModel model);

        /// <summary>
        /// Получение списка записей материально-технических ценностей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<MaterialTechnicalValueRecordPageViewModel> GetMaterialTechnicalValueRecords(MaterialTechnicalValueRecordGetBindingModel model);

        /// <summary>
        /// Получения записи материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<MaterialTechnicalValueRecordViewModel> GetMaterialTechnicalValueRecord(MaterialTechnicalValueRecordGetBindingModel model);

        /// <summary>
        /// Создание новой записи материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateMaterialTechnicalValueRecord(MaterialTechnicalValueRecordRecordBindingModel model);

        /// <summary>
        /// Изменение записи материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateMaterialTechnicalValueRecord(MaterialTechnicalValueRecordRecordBindingModel model);

        /// <summary>
        /// Удаление записи материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteMaterialTechnicalValueRecord(MaterialTechnicalValueRecordRecordBindingModel model);
    }
}
