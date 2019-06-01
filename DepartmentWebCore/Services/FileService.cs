using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Services
{
    public class FileService
    {
        private static string RootPath => @"D:\Department\";

        private static string EventPath => $@"{RootPath}Events\";

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
    }
}
