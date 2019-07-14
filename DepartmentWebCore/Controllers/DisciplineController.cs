using BaseInterfaces.Interfaces;
using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Controllers
{
    public class DisciplineController : Controller
    {
        private string Path => @"D:\Department\";

        private IWebProcessService _serviceWP;

        private IDisciplineService _serviceD;

        public DisciplineController(IWebProcessService serviceWP, IDisciplineService serviceD)
        {
            _serviceWP = serviceWP;
            _serviceD = serviceD;
        }

        // GET: Discipline
        public ActionResult Index(Guid id)
        {
            var list = DisciplineService.GetDisciplinesByCourses(new BaseInterfaces.BindingModels.DisciplineGetBindingModel
            {
                ContingentId = id
            }).Result;

            return View(list);
        }

        
        public ActionResult DisContent(string id)
        {            
            var dis = Services.DisciplineService.GetDiscipline(new BaseInterfaces.BindingModels.DisciplineGetBindingModel() { DisciplineName = id });
            
            if(dis.Result.Count != 0)
            {
                var tmp = _serviceWP.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
                { DisciplineName = dis.Result.FirstOrDefault().DisciplineName });

                if (tmp.StatusCode == Enums.ResultServiceStatusCode.Error)
                {
                    _serviceWP.CreateFolderDis(dis.Result);
                    tmp = _serviceWP.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
                    { DisciplineName = dis.Result.FirstOrDefault().DisciplineName });
                }

                foreach (var item in dis.Result.Select(x => new { LecturerName = x.LecturerName }).GroupBy(x => x.LecturerName))
                {
                    tmp.Result.LecturerName += item.Key + " ";
                }

                return View(tmp.Result);
            }
            else
            {
                return RedirectToAction("Error", "Shared", new { message = "Дисциплина не назначена преподавателю" }, null);//Отобразить ошибку "дисциплина не назначена преподавателю"
            }
            
        }

        [Authorize(Roles = "Преподаватель")]
        public ActionResult LoadFile(string name)
        {
            var tmp = _serviceWP.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
            { DisciplineName = name }).Result;

            var listSelect = new List<WebProcessFileForDownloadViewModel>();

            foreach (var semestr in tmp.Semestrs)
            {
                foreach (var timenorm in semestr.TimeNorms)
                {
                    listSelect.Add(new WebProcessFileForDownloadViewModel
                    {
                        Name = $"{semestr.Name} - {timenorm.Name}",
                        Path = $@"{tmp.Name}\{semestr.Name}\{timenorm.Name}"
                    });
                }
            }

            return View(listSelect);
        }

        [Authorize]
        public FileResult Download(string path, string fileName)
        {
            return File(FileService.GetFileByPathForDiscipline(path), "application/vnd.ms-powerpoint", fileName);
        }

        [Authorize]
        public FileResult PDF(string path, string fileName)
        {
            return File(FileService.GetFileByPathForDiscipline(path), "application/pdf");
        }

        [Authorize(Roles = "Преподаватель")]
        public IActionResult DeleteFile(string path)
        {
            FileService.DeleteFileByPathForDiscipline(path);
            return RedirectToAction("DisContent", "Discipline", new { id = path.Split('\\')[0] }, null);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LoadFile(IFormFileCollection files)
        {
            string path = Request.Form["direction"].ToString();
            await FileService.SaveFilesForDiscipline(files, path);
            return RedirectToAction("DisContent", "Discipline", new { id = path.Split('\\')[0] }, null);
        }       


    }
}
