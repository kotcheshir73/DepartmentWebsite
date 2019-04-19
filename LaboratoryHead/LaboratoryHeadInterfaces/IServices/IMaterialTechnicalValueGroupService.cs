using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.ViewModels;
using Tools;

namespace LaboratoryHeadInterfaces.IServices
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
        ResultService CreateMaterialTechnicalValueGroup(MaterialTechnicalValueGroupSetBindingModel model);

        /// <summary>
        /// Изменение группы описаний материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateMaterialTechnicalValueGroup(MaterialTechnicalValueGroupSetBindingModel model);

        /// <summary>
        /// Удаление группы описаний материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteMaterialTechnicalValueGroup(MaterialTechnicalValueGroupSetBindingModel model);
    }
}