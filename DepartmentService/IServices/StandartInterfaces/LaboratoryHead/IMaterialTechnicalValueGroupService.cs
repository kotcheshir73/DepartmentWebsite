using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface IMaterialTechnicalValueGroupService
    {
        /// <summary>
        /// Получение списка групп описаний материально-технических ценностей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<MaterialTechnicalValueGroupPageViewModel> GetMaterialTechnicalValueGroups(MaterialTechnicalValueGroupGetBindingModel model);

        /// <summary>
        /// Получения группы описаний материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<MaterialTechnicalValueGroupViewModel> GetMaterialTechnicalValueGroup(MaterialTechnicalValueGroupGetBindingModel model);

        /// <summary>
        /// Создание новой группы описаний материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateMaterialTechnicalValueGroup(MaterialTechnicalValueGroupRecordBindingModel model);

        /// <summary>
        /// Изменение группы описаний материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateMaterialTechnicalValueGroup(MaterialTechnicalValueGroupRecordBindingModel model);

        /// <summary>
        /// Удаление группы описаний материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteMaterialTechnicalValueGroup(MaterialTechnicalValueGroupRecordBindingModel model);
    }
}
