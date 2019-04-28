using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepartmentWeb.Controllers
{
    public class LecturersController : Controller
    {

        private ILecturerService _serviceL;

        public LecturersController( ILecturerService serviceL)
        {            
            _serviceL = serviceL;
        }

        // GET: Lecturer
        public ActionResult Index()
        {
            var list = DepartmentWeb.Services.LecturerService.GetLecturers(new BaseInterfaces.BindingModels.LecturerGetBindingModel());

            return View(list.Result.List);
        }

        
        public ActionResult Lecturer(string id)
        {
            var model = Services.LecturerService.GetLecturer(new BaseInterfaces.BindingModels.LecturerGetBindingModel() { Id = new Guid(id) });

            return View(model.Result);
        }

        public ActionResult LecturerDis(string id)
        {
            var list = Services.DisciplineService.GetDisciplines(new BaseInterfaces.BindingModels.LecturerGetBindingModel() { Id = new Guid(id) });

            return PartialView("LecturerDis", list.Result);
        }
    }
}