using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Services
{
    public class FileService
    {
        /// <summary>
        /// Сохранение файлов на диск
        /// </summary>
        /// <param name="files">Файлы</param>
        /// <param name="direction">Путь до папки</param>
        /// <returns></returns>
        public async Task SaveFiles(List<IFormFile> files, string direction)
        {
            if(!Directory.Exists(direction))
            {
                Directory.CreateDirectory(direction);
            }

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream($"{direction}\\{formFile.FileName}", FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
        }

        /// <summary>
        /// Получение списка файлов в папке
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public List<string> GetFiles(string direction)
        {
            var dirInfo = new DirectoryInfo(direction);

            if (!dirInfo.Exists)
            {
                return null;
            }

            return dirInfo.GetFiles().Select(x => x.Name).ToList();
        }

        /// <summary>
        /// Получение файла для скачивания
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] GetFileForDowmload(string path)
        {
            return File.ReadAllBytes(path);
        }

        /// <summary>
        /// Получение типа скачиваемого файла
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetContentType(string filename)
        {
            var types = new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".ppt", "application/vnd.ms-powerpoint"},
                {".pptx", "application/vnd.ms-powerpoint"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".7z", "application/zip"},
                {".zip", "application/zip"},
                {".rar", "application/zip"}
            }; ;
            var ext = Path.GetExtension(filename).ToLowerInvariant();
            return types[ext];
        }

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="path"></param>
        public void DeleteFile(string path)
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// Удаление папки
        /// </summary>
        /// <param name="direction"></param>
        public void DeleteDirection(string direction)
        {
            var dirInfo = new DirectoryInfo(direction);

            if (dirInfo.Exists)
            {
                dirInfo.Delete(true);
            }
        }

        /// <summary>
        /// Получение списка папок и файлов по дисциплине
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="folders">Список папок, если папки нет</param>
        /// <returns></returns>
        public DisciplineContextElementModel GetDisciplineContext(Guid id, string direction, IWebDisciplineService serviceD)
        {
            var directoryInfo = new DirectoryInfo(direction);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                var folders = serviceD.GetDisciplineFolderNames(new WebDisciplineFolderNamesBindingModel { DisciplineId = id }).Result;
                if(folders != null)
                {
                    foreach(var folder in folders)
                    {
                        Directory.CreateDirectory($"{direction}\\{folder.Semester}");

                        foreach(var child in folder.FolderNames)
                        {
                            Directory.CreateDirectory($"{direction}\\{folder.Semester}\\{child}");
                        }
                        Directory.CreateDirectory($"{direction}\\{folder.Semester}\\Дополнительно");
                    }
                }
            }

            var element = new DisciplineContextElementModel();

            GetDirectoriesFromContext(directoryInfo, element, direction);

            return element;
        }

        /// <summary>
        /// Получение списка подкаталогов каталога
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private DisciplineContextElementModel GetDirectoriesFromContext(DirectoryInfo directoryInfo, DisciplineContextElementModel element, string direction)
        {
            var directories = directoryInfo.GetDirectories();
            if(directories.Length > 0)
            {
                element.Childs = element.Childs ?? new List<DisciplineContextElementModel>();

                foreach (var directory in directories)
                {
                    var child = new DisciplineContextElementModel
                    {
                        FullPath = directory.FullName.Substring(direction.Replace("\\\\", "\\").Length),
                        Name = directory.Name,
                        IsFile = false
                    };

                    GetDirectoriesFromContext(directory, child, direction);

                    element.Childs.Add(child);
                }
            }

            GetFilesFromContent(directoryInfo, element, direction);

            return element;
        }

        /// <summary>
        /// получение списка файлов каталога
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="element"></param>
        private void GetFilesFromContent(DirectoryInfo directoryInfo, DisciplineContextElementModel element, string direction)
        {
            var files = directoryInfo.GetFiles();
            if (files.Length > 0)
            {
                element.Childs = element.Childs ?? new List<DisciplineContextElementModel>();

                foreach (var file in files)
                {
                    element.Childs.Add(new DisciplineContextElementModel
                    {
                        FullPath = file.FullName.Substring(direction.Replace("\\\\", "\\").Length),
                        Name = file.Name,
                        IsFile = true
                    });
                }
            }
        }

        /// <summary>
        /// Получение название файла из пути
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }
    }
}