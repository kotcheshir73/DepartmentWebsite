using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using DepartmentWebCore.Models;
using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    public class AcademicPlanRecordMissionController : Controller
    {
        private readonly IAcademicPlanRecordMissionService _serviceAPRM;

        private readonly IAcademicPlanRecordElementService _serviceAPRE;

        private readonly BaseService _baseService;

        private static IStudyProcessService _serviceSP;

        private const string defaultMenu = "AcademicPlanRecordMission";

        public AcademicPlanRecordMissionController(IAcademicPlanRecordMissionService serviceAPRM, IAcademicPlanRecordElementService serviceAPRE,
            BaseService baseService, IStudyProcessService serviceSP)
        {
            _serviceAPRM = serviceAPRM;
            _serviceAPRE = serviceAPRE;
            _baseService = baseService;
            _serviceSP = serviceSP;
        }

        public IActionResult View(Guid Id)
        {
            var academicPlanRecordMission = _serviceAPRM.GetAcademicPlanRecordMission(new AcademicPlanRecordMissionGetBindingModel { Id = Id });
            if (academicPlanRecordMission.Succeeded)
            {
                var academicPlanRecordElement = _serviceAPRE.GetAcademicPlanRecordElement(new AcademicPlanRecordElementGetBindingModel { Id = academicPlanRecordMission.Result.AcademicPlanRecordElementId });
                var lecturers = _baseService.GetLecturers();
                if (academicPlanRecordElement.Succeeded && lecturers != null)
                {
                    var academicPlanRecordElements = _serviceAPRE.GetAcademicPlanRecordElements(new AcademicPlanRecordElementGetBindingModel { AcademicPlanRecordId = academicPlanRecordElement.Result.AcademicPlanRecordId });
                    if (academicPlanRecordElements.Succeeded)
                    {
                        ViewBag.AcademicPlanRecordElements = new SelectList(academicPlanRecordElements.Result.List, "Id", "Discipline");
                        ViewBag.Lecturers = new SelectList(lecturers.OrderBy(x => x.LastName), "Id", "FullName");

                        ViewBag.menuElement = defaultMenu;
                        return View("../StudyProcess/AcademicPlanRecordMission", academicPlanRecordMission.Result);
                    }
                }
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Create(Guid Id)
        {
            var academicPlanRecordMissionView = new AcademicPlanRecordMissionViewModel() { AcademicPlanRecordElementId = Id };
            var academicPlanRecordElement = _serviceAPRE.GetAcademicPlanRecordElement(new AcademicPlanRecordElementGetBindingModel { Id = Id });
            var lecturers = _baseService.GetLecturers();
            if (academicPlanRecordElement.Succeeded && lecturers != null)
            {
                var academicPlanRecordElements = _serviceAPRE.GetAcademicPlanRecordElements(new AcademicPlanRecordElementGetBindingModel { AcademicPlanRecordId = academicPlanRecordElement.Result.AcademicPlanRecordId });
                if (academicPlanRecordElements.Succeeded)
                {
                    ViewBag.AcademicPlanRecordElements = new SelectList(academicPlanRecordElements.Result.List, "Id", "Discipline");
                    ViewBag.Lecturers = new SelectList(lecturers.OrderBy(x => x.LastName), "Id", "FullName");

                    ViewBag.menuElement = defaultMenu;
                    return View("../StudyProcess/AcademicPlanRecordMission", academicPlanRecordMissionView);
                }
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public IActionResult Delete(Guid Id)
        {
            var result = _serviceAPRM.DeleteAcademicPlanRecordMission(new AcademicPlanRecordMissionGetBindingModel { Id = Id });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }
            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult Save(AcademicPlanRecordMissionSetBindingModel model, string Hours)
        {
            ResultService result;
            string error = string.Empty;

            if (model?.Id == Guid.Empty)
            {
                result = _serviceAPRM.CreateAcademicPlanRecordMission(new AcademicPlanRecordMissionSetBindingModel
                {
                    AcademicPlanRecordElementId = model.AcademicPlanRecordElementId,
                    LecturerId = model.LecturerId,
                    Hours = decimal.Parse(Hours.Replace('.', ','))
                });
            }
            else
            {
                result = _serviceAPRM.UpdateAcademicPlanRecordMission(new AcademicPlanRecordMissionSetBindingModel
                {
                    Id = model.Id,
                    AcademicPlanRecordElementId = model.AcademicPlanRecordElementId,
                    LecturerId = model.LecturerId,
                    Hours = decimal.Parse(Hours.Replace('.', ','))
                });
            }

            if (result.Succeeded)
            {
                if (result.Result != null)
                {
                    if (result.Result is Guid id)
                    {
                        model.Id = id;
                    }
                }
            }
            else
            {
                error = result.Errors.LastOrDefault().Value;
            }

            return Json(new Dictionary<string, object> { { "id", model.Id }, { "error", error } });
        }

        public IActionResult Table(Guid Id)
        {
            var names = _serviceSP.GetPropertiesNames(typeof(AcademicPlanRecordMissionViewModel));
            var tableHead = names.displayNames;

            List<List<object>> tableBody = new List<List<object>>();
            List<(bool IsDropdownElement, MenuElementModel action)> actions = new List<(bool IsDropdownElement, MenuElementModel action)>();

            if (Id != Guid.Empty)
            {
                var academicPlanRecordMissions = _serviceAPRM.GetAcademicPlanRecordMissions(new AcademicPlanRecordMissionGetBindingModel { AcademicPlanRecordElementId = Id });
                if (academicPlanRecordMissions.Succeeded)
                {
                    tableBody = _serviceSP.GetPropertiesValues(academicPlanRecordMissions.Result.List, names.propertiesNames);
                }
            }

            ViewBag.menuElement = defaultMenu;
            return PartialView("../StudyProcess/Table", (tableHead, tableBody, actions, Id));
        }
    }
}
