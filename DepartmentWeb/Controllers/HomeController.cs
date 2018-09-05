using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DepartmentModel;
using DepartmentService;
using DepartmentService.Context;
using DepartmentService.Services;
using Unity;
using Unity.Attributes;

namespace DepartmentWeb.Controllers
{
    public class HomeController : Controller
    {
        DepartmentDbContext departmentDbContext = new DepartmentDbContext();

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

        public ActionResult Statement()
        {
            ViewBag.Message = "Your statement page.";
            
            return View(departmentDbContext.AcademicPlanRecords);
        }
    }
}