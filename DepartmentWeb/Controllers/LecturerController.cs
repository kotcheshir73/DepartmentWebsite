using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepartmentWeb.Controllers
{
    public class LecturerController : Controller
    {

        private ILecturerService _serviceL;

        public LecturerController( ILecturerService serviceL)
        {            
            _serviceL = serviceL;
        }

        // GET: Lecturer
        public ActionResult Index()
        {
            return View();
        }

        public List<LecturerViewModel> GetLecturers()
        {
            var list = _serviceL.GetLecturers(new BaseInterfaces.BindingModels.LecturerGetBindingModel());
            return list.Result.List;
        }
    }
}