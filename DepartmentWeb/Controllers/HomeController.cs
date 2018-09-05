using DepartmentService;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepartmentWeb.Controllers
{
    public class HomeController : Controller
    {
        private IEducationalProcessService _serviceEP;

        public HomeController(IEducationalProcessService serviceEP, IAcademicPlanRecordElementService serviceAPRE)
        {
            AccessCheckService.Login("admin", "qwerty");

            _serviceEP = serviceEP;
            /*
            var tmp = serviceAPRE.GetAcademicPlanRecordElement(new DepartmentService.BindingModels.AcademicPlanRecordElementGetBindingModel()
            {
                Id = new Guid("F297DC8B-8616-42ED-A1F8-043EFF53260D")
            });     */  //тестирование подключения к бд
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}