using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    [Authorize(Roles = Startup.StudyProcessAuthRole)]
    public class DistributionController : Controller
    {
        private readonly ITimeNormService _serviceTN;

        private readonly ILecturerService _serviceL;

        private readonly IAcademicYearService _serviceAY;

        private readonly IAcademicYearProcess _process;

        private readonly IAcademicPlanRecordMissionService _serviceAPRM;

        private readonly IStudyProcessService _serviceSP;

        public DistributionController(ITimeNormService serviceTN, ILecturerService serviceL,
            IAcademicYearProcess process, IStudyProcessService serviceSP, IAcademicYearService serviceAY,
            IAcademicPlanRecordMissionService serviceAPRM)
        {
            _serviceTN = serviceTN;
            _serviceL = serviceL;
            _process = process;
            _serviceSP = serviceSP;
            _serviceAY = serviceAY;
            _serviceAPRM = serviceAPRM;
        }

        public IActionResult View(Guid Id)
        {
            var lecturers = _serviceL.GetLecturers(new LecturerGetBindingModel());
            var result = _serviceSP.GetAcademicYearLoading(new AcademicYearGetBindingModel { Id = Id });
            var academicYear = _serviceAY.GetAcademicYear(new AcademicYearGetBindingModel { Id = Id });
            if (result.Succeeded && lecturers.Succeeded && academicYear.Succeeded)
            {
                var data = result.Result;
                ViewBag.Data = data;
                ViewBag.Lecturers = new SelectList(lecturers.Result.List.OrderBy(x => x.LastName), "Id", "FullName");
                ViewBag.AcademicYearId = Id;
                ViewBag.Title = academicYear.Result.Title + ": Расчет штатов";
            }
            return View("../StudyProcess/Distribution");
        }

        [HttpPost]
        public IActionResult GetLecturerMissions(Guid LecturerId, Guid AcademicYearId)
        {
            string error = string.Empty;
            object result = null;

            var missions = _serviceSP.GetLecturerMissions(LecturerId, AcademicYearId);
            var lecturer = _serviceL.GetLecturer(new LecturerGetBindingModel { Id = LecturerId });
            if (missions.Succeeded && lecturer.Succeeded)
            {
                missions.Result.Insert(0, lecturer.Result.Id);
                missions.Result.Insert(1, lecturer.Result.FullName);
                result = missions.Result;
            }
            else
            {
                error = missions.Errors.LastOrDefault().Value + ";" + lecturer.Errors.LastOrDefault().Value;
            }

            return Json(new Dictionary<string, object> { { "info", result }, { "error", error } });
        }

        [HttpPost]
        public IActionResult GetListAPRM(Guid LecturerId, Guid AcademicPlanRecordId, Guid AcademicYearId)
        {
            string error = string.Empty;
            object result = null;

            var lecturer = _serviceL.GetLecturer(new LecturerGetBindingModel { Id = LecturerId });
            var list = _process.GetListAPRM(new AcademicYearGetBindingModel() { Id = AcademicYearId },
                                            new AcademicPlanRecordGetBindingModel() { Id = AcademicPlanRecordId },
                                            new LecturerGetBindingModel() { Id = LecturerId });
            if (lecturer.Succeeded && list.Succeeded)
            {
                result = list.Result;
            }
            else
            {
                error = list.Errors.LastOrDefault().Value;
            }

            return Json(new Dictionary<string, object> { { "info", result }, { "error", error } });
        }

        [HttpPost]
        public IActionResult SaveAPRMs(string[][] APRMs)
        {
            string error = string.Empty;
            ResultService result;

            foreach (var element in APRMs)
            {
                if (element[0] == null && element[3] == null)
                {
                    continue;
                }

                if (element[0] == null)
                {
                    result = _serviceAPRM.CreateAcademicPlanRecordMission(new AcademicPlanRecordMissionSetBindingModel
                    {
                        AcademicPlanRecordElementId = new Guid(element[1]),
                        LecturerId = new Guid(element[2]),
                        Hours = Convert.ToDecimal(element[3])
                    });
                }
                else
                {
                    result = _serviceAPRM.UpdateAcademicPlanRecordMission(new AcademicPlanRecordMissionSetBindingModel
                    {
                        Id = new Guid(element[0]),
                        AcademicPlanRecordElementId = new Guid(element[1]),
                        LecturerId = new Guid(element[2]),
                        Hours = Convert.ToDecimal(element[3])
                    });

                }

                if (!result.Succeeded)
                {
                    error += result.Errors.LastOrDefault().Value + " ";
                }
            }

            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult CalcFactHoursForAcademicYear(Guid AcademicYearId)
        {
            string error = string.Empty;

            ResultService result = _process.CalcFactHoursForAcademicYear(new AcademicYearGetBindingModel { Id = AcademicYearId });
            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }

            return Json(new Dictionary<string, object> { { "error", error } });
        }
    }
}
