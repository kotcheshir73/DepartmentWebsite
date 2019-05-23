using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Authorization;
using AcademicYearInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    public class HomeController : Controller
    {
        private IAcademicYearProcess _process;

        private IAcademicYearService _serviceAY;

        public HomeController(IAcademicYearProcess process, IAcademicYearService serviceAY)
        {
            //Tools.DepartmentUserManager.Login("admin", "qwerty");

            _process = process;

            _serviceAY = serviceAY;

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

        public ActionResult NIR()
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
