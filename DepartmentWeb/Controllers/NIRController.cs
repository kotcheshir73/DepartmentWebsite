using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DepartmentService;
using DepartmentService.IServices;

namespace DepartmentWeb.Controllers
{
    public class NIRController : Controller
    {
        private IIndividualPlanRecordService _serviceIPRS;
        public NIRController(IIndividualPlanRecordService serviceIPRS)
        {
            _serviceIPRS = serviceIPRS;
            /*
            var tmp = serviceAPRE.GetAcademicPlanRecordElement(new DepartmentService.BindingModels.AcademicPlanRecordElementGetBindingModel()
            {
                Id = new Guid("F297DC8B-8616-42ED-A1F8-043EFF53260D")
            });     */  //тестирование подключения к бд
        }
        // GET: NIR
        public ActionResult Index()
        {
            var tmp = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("0C6437D0-6F39-40E8-BB99-16B45D317A72"),
                Title = "Научно-исследовательская работа"
            });

            return View(tmp.Result);
        }
    }
}