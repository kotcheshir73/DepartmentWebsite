using BaseInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace DepartmentWeb.Controllers
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
            ViewBag.DisId = id;
            var dis = Services.DisciplineService.GetDiscipline(new BaseInterfaces.BindingModels.DisciplineGetBindingModel() { Id = new Guid(id)});
            //var list = Services.DisciplineService.GetDisciplines(new BaseInterfaces.BindingModels.LecturerGetBindingModel() { Id = new Guid(id) });
            

            var tmp = _serviceWP.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
                { DisciplineName = dis.Result.FirstOrDefault().DisciplineName });
            return View(tmp.Result);
        }

        public ActionResult LoadFile(string name)
        {
            var tmp = _serviceWP.GetDisciplineForDownload(new WebInterfaces.BindingModels.WebProcessDisciplineForDownloadGetBindingModel()
                { DisciplineName = name }).Result;

            var listSelect = new List<WebProcessFileForDownloadViewModel>();

            foreach(var semestr in tmp.Semestrs)
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
            return File(Path + path, "application/pdf");
        }

        [HttpPost]
        public ActionResult LoadFile(HttpPostedFileBase[] file1)
        {
            foreach (var file in file1)
            {
                //file.SaveAs(Server.MapPath("~/Files/" + Request.Form["direction"].ToString() + "//" + file.FileName)); //сохранение на сервер
                file.SaveAs(@"D:\Department\" + Request.Form["direction"].ToString() + "\\" + file.FileName); //сохранение по физическому пути
            }
            return RedirectToAction("DisContent", "Discipline", ViewBag.DisId);
        }        
    }
}