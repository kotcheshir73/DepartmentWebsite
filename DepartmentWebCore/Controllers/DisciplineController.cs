using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Controllers
{
    public class DisciplineController : Controller
    {
        private string Path => @"D:\Department\";

        private IWebProcess _process;

        private IWebDisciplineService _serviceWD;

        public DisciplineController(IWebProcess process, IWebDisciplineService serviceWD)
        {
            _process = process;
            _serviceWD = serviceWD;
        }

        public ActionResult Discipline(Guid id)
        {
            var model = _serviceWD.GetDiscipline(new WebDisciplineGetBindingModel { Id = id });

            if (!model.Succeeded)
            {
                return new EmptyResult();
            }

            return View(model.Result);
        }

        
        public ActionResult DisContent(string id)
        {            
            var dis = Services.DisciplineService.GetDiscipline(new BaseInterfaces.BindingModels.DisciplineGetBindingModel() { DisciplineName = id });
            
            if(dis.Result.Count != 0)
            {
                var tmp = _process.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
                { DisciplineName = dis.Result.FirstOrDefault().DisciplineName });

                if (tmp.StatusCode == Enums.ResultServiceStatusCode.Error)
                {
                    _process.CreateFolderDis(dis.Result);
                    tmp = _process.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
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
            var tmp = _process.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
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
