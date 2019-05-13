using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepartmentWeb.Controllers
{
    public class DisciplineController : Controller
    {
        // GET: Discipline
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisContent(string id)
        {
            //var list = Services.DisciplineService.GetDisciplines(new BaseInterfaces.BindingModels.LecturerGetBindingModel() { Id = new Guid(id) });

            return View();
        }
    }
}