using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DepartmentService;
using DepartmentService.IServices;

namespace DepartmentWeb.Controllers
{
    public class IndividualPlanController : Controller
    {
        private IIndividualPlanTitleService _serviceIPTS;

        public IndividualPlanController(IIndividualPlanTitleService serviceIPTS)
        {
            _serviceIPTS = serviceIPTS;
            /*
            var tmp = serviceAPRE.GetAcademicPlanRecordElement(new DepartmentService.BindingModels.AcademicPlanRecordElementGetBindingModel()
            {
                Id = new Guid("F297DC8B-8616-42ED-A1F8-043EFF53260D")
            });     */  //тестирование подключения к бд
        }
        // GET: IndividualPlan
        public ActionResult Index()
        {
            var tmp = _serviceIPTS.GetIndividualPlanTitles(new DepartmentService.BindingModels.IndividualPlanTitleGetBindingModel());
            return View(tmp.Result);
        }
    }
}