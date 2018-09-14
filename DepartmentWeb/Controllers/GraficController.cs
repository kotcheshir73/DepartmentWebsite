using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepartmentWeb.Controllers
{
    public class GraficController : Controller
    {
        private IStudentGroupService _serviceSG;
        private IDisciplineService _serviceD;

        public GraficController(IStudentGroupService serviceSG, IDisciplineService serviceD)
        {
            _serviceSG = serviceSG;
            _serviceD = serviceD;

        }
        // GET: Statement
        public ActionResult Index()
        {
            var tmp = _serviceSG.GetStudentGroups(new DepartmentService.BindingModels.StudentGroupGetBindingModel());
            return View(tmp.Result);
        }
    }
}