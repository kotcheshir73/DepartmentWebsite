using BaseInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInterfaces.Interfaces;

namespace DepartmentWeb.Controllers
{
    public class DisciplineController : Controller
    {
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
            var dis = Services.DisciplineService.GetDiscipline(new BaseInterfaces.BindingModels.DisciplineGetBindingModel() { Id = new Guid(id)});
            //var list = Services.DisciplineService.GetDisciplines(new BaseInterfaces.BindingModels.LecturerGetBindingModel() { Id = new Guid(id) });
            _serviceWP.CreateFolderDis(dis.Result);
            return View();
        }

        public ActionResult LoadFile()
        {
            return View();
        }
    }
}