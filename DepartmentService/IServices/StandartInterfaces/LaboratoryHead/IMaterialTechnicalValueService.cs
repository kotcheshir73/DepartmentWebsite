﻿using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
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
        ResultService CreateMaterialTechnicalValue(MaterialTechnicalValueRecordBindingModel model);

        /// <summary>
        /// Изменение материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateMaterialTechnicalValue(MaterialTechnicalValueRecordBindingModel model);

        /// <summary>
        /// Удаление материально-технической ценности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteMaterialTechnicalValue(MaterialTechnicalValueRecordBindingModel model);
    }
}