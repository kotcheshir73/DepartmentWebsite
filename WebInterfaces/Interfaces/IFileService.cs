using WebInterfaces.BindingModels;
using WebInterfaces.ViewModels;
using Tools;

namespace BaseInterfaces.Interfaces
{
    public interface IFileService
    {
		/// <summary>
		/// Получение списка файлов
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService<FilePageViewModel> GetFiles(FileGetBindingModel model);

		/// <summary>
		/// Получение файла
		/// </summary>
		/// <param name="model">Идентификатор файла</param>
		/// <returns></returns>
		ResultService<FileViewModel> GetFile(FileGetBindingModel model);

		/// <summary>
		/// Создание файла
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService CreateFile(FileSetBindingModel model);

		/// <summary>
		/// Изменение файла
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		ResultService UpdateFile(FileSetBindingModel model);

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultService DeleteFile(FileGetBindingModel model);
    }
}