using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface ISoftwareService
    {
        /// <summary>
        /// Получение списка ПО
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<SoftwarePageViewModel> GetSoftwares(SoftwareGetBindingModel model);

        /// <summary>
        /// Получения ПО
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<SoftwareViewModel> GetSoftware(SoftwareGetBindingModel model);

        /// <summary>
        /// Создание ПО
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateSoftware(SoftwareSetBindingModel model);

        /// <summary>
        /// Изменение ПО
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateSoftware(SoftwareSetBindingModel model);

        /// <summary>
        /// Удаление ПО
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteSoftware(SoftwareSetBindingModel model);
    }
}
