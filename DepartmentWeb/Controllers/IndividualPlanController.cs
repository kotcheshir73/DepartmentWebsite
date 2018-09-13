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
        private IIndividualPlanKindOfWorkService _serviceIPKWS;
        private IIndividualPlanRecordService _serviceIPRS;

        public IndividualPlanController(IIndividualPlanTitleService serviceIPTS, IIndividualPlanKindOfWorkService serviceIPKWS, IIndividualPlanRecordService serviceIPRS)
        {
            _serviceIPTS = serviceIPTS;
            _serviceIPKWS = serviceIPKWS;
            _serviceIPRS = serviceIPRS;
            /*
            var tmp = serviceAPRE.GetAcademicPlanRecordElement(new DepartmentService.BindingModels.AcademicPlanRecordElementGetBindingModel()
            {
                Id = new Guid("F297DC8B-8616-42ED-A1F8-043EFF53260D")
            });     */  //тестирование подключения к бд
        }
        // GET: IndividualPlan


        public ActionResult Metodich()
        {

            //    // добавление в БД

            //    _serviceIPKWS.CreateIndividualPlanKindOfWork(new DepartmentService.BindingModels.IndividualPlanKindOfWorkSetBindingModel()
            //    {
            //        IndividualPlanTitleId = new Guid("36F788E2-B808-4179-9CEE-70810EB8C90E"),
            //        Name = " ",
            //        TimeNormDescription = " "
            //    }
            //);
            //return View();

            var tmp = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                Title = "Методическая работа"
            });
            return View(tmp.Result);
        }



        public ActionResult Organizac()
        {
            var tmp = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                Title = "Организационная работа"
            });

            return View(tmp.Result);
        }

        public ActionResult Vospit()
        {
            var tmp = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                Title = "Воспитательная работа"
            });

            return View(tmp.Result);
        }
    }
}