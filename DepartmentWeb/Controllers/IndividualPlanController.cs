using DepartmentService;
using DepartmentService.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepartmentWeb.Controllers
{
    public class IndividualPlanController : Controller
    {
        private IEducationalProcessService _serviceEP;

        public IndividualPlanController(IEducationalProcessService serviceEP, IAcademicPlanRecordElementService serviceAPRE)
        {
            _serviceEP = serviceEP;
            /*
            var tmp = serviceAPRE.GetAcademicPlanRecordElement(new DepartmentService.BindingModels.AcademicPlanRecordElementGetBindingModel()
            {
                Id = new Guid("F297DC8B-8616-42ED-A1F8-043EFF53260D")
            });     */  //тестирование подключения к бд
        }
        // GET: IndividualPlan
        public ActionResult Metodich()
        {
            return View();
        }
    }
}