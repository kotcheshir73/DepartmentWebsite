using BaseInterfaces.Interfaces;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisContent(string id)
        {
            if(id != null)
                ViewBag.DisId = id;//TODO:Это не доделано
            var dis = Services.DisciplineService.GetDiscipline(new BaseInterfaces.BindingModels.DisciplineGetBindingModel() { DisciplineName = id });

            var tmp = _serviceWP.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
            { DisciplineName = dis.Result.FirstOrDefault().DisciplineName });

            if(tmp.StatusCode == Enums.ResultServiceStatusCode.Error)
            {
                _serviceWP.CreateFolderDis(dis.Result);
                tmp = _serviceWP.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
                { DisciplineName = dis.Result.FirstOrDefault().DisciplineName });
            }

            foreach(var item in dis.Result.Select(x => new { LecturerName = x.LecturerName }).GroupBy(x => x.LecturerName))
            {
                tmp.Result.LecturerName += item.Key + " ";
            }
            


            return View(tmp.Result);
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

        public FileResult Download(string path, string fileName)
        {
            var doc = new byte[0];
            doc = System.IO.File.ReadAllBytes(Path + path);
            return File(doc, "application/vnd.ms-powerpoint", fileName);
        }

        public FileResult PDF(string path, string fileName)
        {
            var doc = new byte[0];
            doc = System.IO.File.ReadAllBytes(Path + path);
            return File(doc, "application/pdf");
        }

        [HttpPost]
        public async Task<IActionResult> LoadFile(IFormFileCollection files)
        {
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(@"D:\Department\" + Request.Form["direction"].ToString() + "\\" + formFile.FileName, FileMode.Create))
                    {
                       await formFile.CopyToAsync(stream);
                    }
                }
            }

            //foreach (var file in file1)
            //{
            //    //file.SaveAs(Server.MapPath("~/Files/" + Request.Form["direction"].ToString() + "//" + file.FileName)); //сохранение на сервер
            //    file..SaveAs(@"D:\Department\" + Request.Form["direction"].ToString() + "\\" + file.FileName); //сохранение по физическому пути
            //}
            return RedirectToAction("Index", "Home");
        }
    }
}
