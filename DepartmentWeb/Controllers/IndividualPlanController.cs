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
            //    _serviceIPTS.CreateIndividualPlanKindOfWork(new DepartmentService.BindingModels.IndividualPlanKindOfWorkSetBindingModel()
            //    {
            //        IndividualPlanTitleId = new Guid("7DCD4019-F67C-40FD-A325-92E5E16ADB98"),
            //        Name = "Профориентационная работа среди школьной, рабочей и сельской молодежи",
            //        TimeNormDescription = "Профориентационная работа среди школьной, рабочей и сельской молодежи по фактическим затратам времени"
            //    }
            //    );
            //_serviceIPRS.CreateIndividualPlanRecord(new DepartmentService.BindingModels.IndividualPlanRecordSetBindingModel()
            //{
            //    IndividualPlanKindOfWorkId = new Guid("AA08B00E-1298-487E-831A-1668CB702020"),
            //    LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
            //    PlanAutumn = 1.0,
            //    PlanSpring=2.0,
            //    FactAutumn = 2.0,
            //    FactSpring = 3.0         
            //}
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
    }
}