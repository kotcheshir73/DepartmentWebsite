using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DepartmentService;
using DepartmentService.IServices;

namespace DepartmentWeb.Controllers
{
    public class StatementController : Controller
    {
        private IAcademicYearService _serviceAY;
        public StatementController(IEducationalProcessService serviceEP, IAcademicYearService serviceAY)
        {
            _serviceAY = serviceAY;
        }
        // GET: Statement
        public ActionResult Index()
        {
            
            var tmp = _serviceAY.GetAcademicYears(new DepartmentService.BindingModels.AcademicYearGetBindingModel());
            
            return View(tmp.Result);
        }
    }
}