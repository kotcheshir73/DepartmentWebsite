using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.ViewModels;
using Tools;

namespace LaboratoryHeadInterfaces.Interfaces
{
    public interface IMaterialTechnicalValueService
    {
        /// <summary>
        /// Получение списка аудиторий
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<ClassroomPageViewModel> GetClassrooms(ClassroomGetBindingModel model);

        /// <summary>
        /// Получение списка материально-технических ценностей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<MaterialTechnicalValuePageViewModel> GetMaterialTechnicalValues(MaterialTechnicalValueGetBindingModel model);

        /// <summary>
        /// Получения материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<MaterialTechnicalValueViewModel> GetMaterialTechnicalValue(MaterialTechnicalValueGetBindingModel model);

        /// <summary>
        /// Создание новой материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateMaterialTechnicalValue(MaterialTechnicalValueSetBindingModel model);

        /// <summary>
        /// Изменение материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateMaterialTechnicalValue(MaterialTechnicalValueSetBindingModel model);

        /// <summary>
        /// Удаление материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteMaterialTechnicalValue(MaterialTechnicalValueSetBindingModel model);
    }
}