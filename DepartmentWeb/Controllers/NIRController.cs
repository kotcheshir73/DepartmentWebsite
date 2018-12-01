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
        }
        
        public ActionResult Index()
        {
            var tmp = _serviceIPRS.GetIndividualPlanRecords(new DepartmentService.BindingModels.IndividualPlanRecordGetBindingModel()
            {
                LecturerId = new Guid("837FF099-55C2-41B9-8B0A-8A341AA51469"),
                Title = "Научно-исследовательская работа"
            });

            return View(tmp.Result.List);
        }

        [HttpPost]
        public ActionResult Index(List<DepartmentService.ViewModels.IndividualPlanRecordViewModel> individualPlanRecordViewModels)
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