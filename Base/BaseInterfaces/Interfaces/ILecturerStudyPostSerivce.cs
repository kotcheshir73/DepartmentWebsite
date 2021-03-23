using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using Tools;

namespace BaseInterfaces.Interfaces
{
    public interface ILecturerStudyPostSerivce
    {
        /// <summary>
        /// Получение списка должностей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerStudyPostPageViewModel> GetLecturerStudyPosts(LecturerStudyPostGetBindingModel model);

        /// <summary>
        /// Получения должности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService<LecturerStudyPostViewModel> GetLecturerStudyPost(LecturerStudyPostGetBindingModel model);

        /// <summary>
        /// Создание новой должности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService CreateLecturerStudyPost(LecturerStudyPostSetBindingModel model);

        /// <summary>
        /// Изменение должности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService UpdateLecturerStudyPost(LecturerStudyPostSetBindingModel model);

        /// <summary>
        /// Удаление должности
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteLecturerStudyPost(LecturerStudyPostGetBindingModel model);
    }
}