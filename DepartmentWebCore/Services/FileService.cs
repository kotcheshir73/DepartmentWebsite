using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Services
{
    public class FileService
    {
        private static string RootPath => @"D:\Department\";

        private static string EventPath => $@"{RootPath}Events\";

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

        public List<string> GetFiles(string direction)
        {
            var dirInfo = new DirectoryInfo(direction);

            if (!dirInfo.Exists)
            {
                return null;
            }

            return dirInfo.GetFiles().Select(x => x.Name).ToList();
        }

        public byte[] GetFileForDowmload(string path)
        {
            return File.ReadAllBytes(path);
        }

        public void DeleteFile(string path)
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void DeleteDirection(string direction)
        {
            var dirInfo = new DirectoryInfo(direction);

            if (dirInfo.Exists)
            {
                dirInfo.Delete(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="direction">Discipline\\Semestr\\TimeNorm</param>
        /// <returns></returns>
        public async static Task SaveFilesForDiscipline(IFormFileCollection files, string direction)
        {
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(RootPath + direction + "\\" + formFile.FileName, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="direction">EventId</param>
        /// <returns></returns>
        public async static Task SaveFilesForEvent(IFormFileCollection files, string direction)
        {
            Directory.CreateDirectory(EventPath + direction);
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(EventPath + direction + "\\" + formFile.FileName, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
        }

        public static List<WebProcessFileForDownloadViewModel> GetFilesForEvent(string EventId)
        {
            List<WebProcessFileForDownloadViewModel> list = new List<WebProcessFileForDownloadViewModel>();
            DirectoryInfo eventDirectory = new DirectoryInfo(EventPath + EventId);
            if (eventDirectory.Exists)
            {
                foreach (var file in eventDirectory.GetFiles())
                {
                    list.Add(new WebProcessFileForDownloadViewModel
                    {
                        Name = file.Name,
                        Path = $"{eventDirectory.Name}\\{file.Name}"
                    });
                }
            }
            return list;
        }

        public static byte[] GetFileByPathForDiscipline(string path)
        {
            return File.ReadAllBytes(RootPath + path);
        }

        public static byte[] GetFileByPathForEvent(string path)
        {
            return File.ReadAllBytes(EventPath + path);
        }

        /// <summary>
        /// Удаление файла дисциплины
        /// </summary>
        /// <param name="path">"Discipline\\Semestr\\TimeNorm\\NameFile"</param>
        public static void DeleteFileByPathForDiscipline(string path)
        {
            File.Delete(RootPath + path);
        }

        /// <summary>
        /// Удаление файла новости
        /// </summary>
        /// <param name="path">"EventId\\NameFile"</param>
        public static void DeleteFileByPathForEvent(string path)
        {
            File.Delete(EventPath + path);
        }

        public static void DeleteDirectoryByPathForEvent(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(EventPath + path);
            if (dir.Exists)
            {
                foreach(var file in dir.GetFiles())
                {
                    file.Delete();
                }
                dir.Delete();
            }
        }

        public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}