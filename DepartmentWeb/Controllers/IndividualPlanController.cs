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
            var tmp = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                Title = "Методическая работа"
            });
            return View(tmp.Result.List);
        }

        [HttpPost]
        public ActionResult Metodich(List<DepartmentService.ViewModels.IndividualPlanRecordViewModel> individualPlanRecordViewModels)
        {

            foreach (var tmp in individualPlanRecordViewModels)
            {
                var element = _serviceIPRS.GetIndividualPlanRecord(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
                {
                    Id = tmp.Id
                });
                _serviceIPRS.UpdateIndividualPlanRecord(new DepartmentService.BindingModels.IndividualPlanRecordSetBindingModel()
                {
                    Id = element.Result.Id,
                    LecturerId =  element.Result.LecturerId,
                    AcademicYearId = element.Result.AcademicYearId,
                    IndividualPlanKindOfWorkId = element.Result.IndividualPlanKindOfWorkId,
                    PlanAutumn = tmp.PlanAutumn,
                    PlanSpring = tmp.PlanSpring,
                    FactAutumn =  tmp.FactAutumn,
                    FactSpring = tmp.FactSpring
                });
            }

            var tmp2 = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                Title = "Методическая работа"
            });

            return View("Metodich", tmp2.Result.List);
        }



        public ActionResult Organizac()
        {
            var tmp = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                Title = "Организационная работа"
            });

            return View(tmp.Result.List);
        }

        [HttpPost]
        public ActionResult Organizac(List<DepartmentService.ViewModels.IndividualPlanRecordViewModel> individualPlanRecordViewModels)
        {

            foreach (var tmp in individualPlanRecordViewModels)
            {
                var element = _serviceIPRS.GetIndividualPlanRecord(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
                {
                    Id = tmp.Id
                });
                _serviceIPRS.UpdateIndividualPlanRecord(new DepartmentService.BindingModels.IndividualPlanRecordSetBindingModel()
                {
                    Id = element.Result.Id,
                    LecturerId = element.Result.LecturerId,
                    AcademicYearId = element.Result.AcademicYearId,
                    IndividualPlanKindOfWorkId = element.Result.IndividualPlanKindOfWorkId,
                    PlanAutumn = tmp.PlanAutumn,
                    PlanSpring = tmp.PlanSpring,
                    FactAutumn = tmp.FactAutumn,
                    FactSpring = tmp.FactSpring
                });
            }

            var tmp2 = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                Title = "Организационная работа"
            });

            return View("Organizac", tmp2.Result.List);
        }


        public ActionResult Vospit()
        {
            var tmp = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                Title = "Воспитательная работа"
            });

            return View(tmp.Result.List);
        }

        [HttpPost]
        public ActionResult Vospit(List<DepartmentService.ViewModels.IndividualPlanRecordViewModel> individualPlanRecordViewModels)
        {

            foreach (var tmp in individualPlanRecordViewModels)
            {
                var element = _serviceIPRS.GetIndividualPlanRecord(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
                {
                    Id = tmp.Id
                });
                _serviceIPRS.UpdateIndividualPlanRecord(new DepartmentService.BindingModels.IndividualPlanRecordSetBindingModel()
                {
                    Id = element.Result.Id,
                    LecturerId = element.Result.LecturerId,
                    AcademicYearId = element.Result.AcademicYearId,
                    IndividualPlanKindOfWorkId = element.Result.IndividualPlanKindOfWorkId,
                    PlanAutumn = tmp.PlanAutumn,
                    PlanSpring = tmp.PlanSpring,
                    FactAutumn = tmp.FactAutumn,
                    FactSpring = tmp.FactSpring
                });
            }

            var tmp2 = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                Title = "Воспитательная работа"
            });

            return View("Vospit", tmp2.Result.List);
        }

        public ActionResult NIR()
        {
            var tmp = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                Title = "Научно-исследовательская работа"
            });

            return View(tmp.Result.List);
        }

        [HttpPost]
        public ActionResult NIR(List<DepartmentService.ViewModels.IndividualPlanRecordViewModel> individualPlanRecordViewModels)
        {

            foreach (var tmp in individualPlanRecordViewModels)
            {
                var element = _serviceIPRS.GetIndividualPlanRecord(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
                {
                    Id = tmp.Id
                });
                _serviceIPRS.UpdateIndividualPlanRecord(new DepartmentService.BindingModels.IndividualPlanRecordSetBindingModel()
                {
                    Id = element.Result.Id,
                    LecturerId = element.Result.LecturerId,
                    AcademicYearId = element.Result.AcademicYearId,
                    IndividualPlanKindOfWorkId = element.Result.IndividualPlanKindOfWorkId,
                    PlanAutumn = tmp.PlanAutumn,
                    PlanSpring = tmp.PlanSpring,
                    FactAutumn = tmp.FactAutumn,
                    FactSpring = tmp.FactSpring
                });
            }

            var tmp2 = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                Title = "Научно-исследовательская работа"
            });

            return View("NIR", tmp2.Result.List);
        }


    }
}