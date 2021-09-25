using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using DepartmentWebCore.Models;
using Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    public class AcademicPlanRecordController : Controller
    {
        private readonly IAcademicPlanService _serviceAP;

        private readonly IAcademicPlanRecordService _serviceAPR;

        private static IStudyProcessService _serviceSP;

        private const string defaultMenu = "AcademicPlanRecord";

        public AcademicPlanRecordController(IAcademicPlanService serviceAP, IAcademicPlanRecordService serviceAPR,
            IStudyProcessService serviceSP)
        {
            _serviceAP = serviceAP;
            _serviceAPR = serviceAPR;
            _serviceSP = serviceSP;
        }

        public IActionResult View(Guid Id)
        {
            var academicPlanRecord = _serviceAPR.GetAcademicPlanRecord(new AcademicPlanRecordGetBindingModel { Id = Id });
            if (academicPlanRecord.Succeeded)
            {
                var academicPlan = _serviceAP.GetAcademicPlan(new AcademicPlanGetBindingModel { Id = academicPlanRecord.Result.AcademicPlanId });
                var academicPlans = _serviceAP.GetAcademicPlans(new AcademicPlanGetBindingModel { AcademicYearId = academicPlan.Result.AcademicYearId });
                if (academicPlan.Succeeded)
                {
                    var disciplines = _serviceAPR.GetDisciplines(new DisciplineGetBindingModel { });
                    var contingents = _serviceAPR.GetContingents(new ContingentGetBindingModel { AcademicPlanId = academicPlan.Result.Id });
                    if (disciplines.Succeeded && contingents.Succeeded)
                    {
                        var semesters = Enum.GetValues(typeof(Semesters))
                            .Cast<Semesters>()
                            .Select(v => v.ToString())
                            .ToList();

                        ViewBag.AcademicPlans = academicPlans.Result.List.Select(x => new SelectListItem()
                        {
                            Value = x.Id.ToString(),
                            Text = x.EducationDirection
                        });

                        ViewBag.Disciplines = new SelectList(disciplines.Result.List, "Id", "DisciplineName");
                        ViewBag.Contingents = new SelectList(contingents.Result.List, "Id", "ContingentName");
                        ViewBag.Semesters = new SelectList(semesters);

                        ViewBag.menuElement = defaultMenu;
                        return View("../StudyProcess/AcademicPlanRecord", academicPlanRecord.Result);
                    }
                }
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Create(Guid Id)
        {
            var academicPlanRecordView = new AcademicPlanRecordViewModel() { AcademicPlanId = Id };
            var academicPlan = _serviceAP.GetAcademicPlan(new AcademicPlanGetBindingModel { Id = Id });
            var academicPlans = _serviceAP.GetAcademicPlans(new AcademicPlanGetBindingModel { AcademicYearId = academicPlan.Result.AcademicYearId });
            if (academicPlan.Succeeded)
            {
                var disciplines = _serviceAPR.GetDisciplines(new DisciplineGetBindingModel { });
                var contingents = _serviceAPR.GetContingents(new ContingentGetBindingModel { AcademicPlanId = academicPlan.Result.Id });
                if (disciplines.Succeeded && contingents.Succeeded)
                {
                    var semesters = Enum.GetValues(typeof(Semesters))
                        .Cast<Semesters>()
                        .Select(v => v.ToString())
                        .ToList();

                    ViewBag.AcademicPlans = academicPlans.Result.List.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.EducationDirection
                    });

                    ViewBag.Disciplines = new SelectList(disciplines.Result.List, "Id", "DisciplineName");
                    ViewBag.Contingents = new SelectList(contingents.Result.List, "Id", "ContingentName");
                    ViewBag.Semesters = new SelectList(semesters);

                    ViewBag.menuElement = defaultMenu;
                    return View("../StudyProcess/AcademicPlanRecord", academicPlanRecordView);
                }
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public IActionResult Delete(Guid Id)
        {
            var result = _serviceAPR.DeleteAcademicPlanRecord(new AcademicPlanRecordGetBindingModel { Id = Id });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }
            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult Save(AcademicPlanRecordSetBindingModel model)
        {
            ResultService result;
            string error = string.Empty;

            if (model?.Id == Guid.Empty)
            {
                result = _serviceAPR.CreateAcademicPlanRecord(new AcademicPlanRecordSetBindingModel
                {
                    AcademicPlanId = model.AcademicPlanId,
                    DisciplineId = model.DisciplineId,
                    ContingentId = model.ContingentId,
                    Semester = model.Semester,
                    Zet = model.Zet,
                    IsUseInWorkload = model.IsUseInWorkload,
                    InDepartment = model.InDepartment,
                    IsActiveSemester = model.IsActiveSemester,
                    IsChild = model.IsChild,
                    IsParent = model.IsParent,
                    IsFacultative = model.IsFacultative
                });
            }
            else
            {
                result = _serviceAPR.UpdateAcademicPlanRecord(new AcademicPlanRecordSetBindingModel
                {
                    Id = model.Id,
                    AcademicPlanId = model.AcademicPlanId,
                    DisciplineId = model.DisciplineId,
                    ContingentId = model.ContingentId,
                    Semester = model.Semester,
                    Zet = model.Zet,
                    IsUseInWorkload = model.IsUseInWorkload,
                    InDepartment = model.InDepartment,
                    IsActiveSemester = model.IsActiveSemester,
                    IsChild = model.IsChild,
                    IsParent = model.IsParent,
                    IsFacultative = model.IsFacultative
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
            var names = _serviceSP.GetPropertiesNames(typeof(AcademicPlanRecordViewModel));
            var tableHead = names.displayNames;

            List<List<object>> tableBody = new List<List<object>>();
            List<(bool IsDropdownElement, MenuElementModel action)> actions = new List<(bool IsDropdownElement, MenuElementModel action)>();

            if (Id != Guid.Empty)
            {
                var academicPlanRecords = _serviceAPR.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { AcademicPlanId = Id });
                var academicPlan = _serviceAP.GetAcademicPlan(new AcademicPlanGetBindingModel { Id = Id });
                if (academicPlanRecords.Succeeded && academicPlan.Succeeded)
                {
                    academicPlanRecords.Result.List = academicPlanRecords.Result.List
                        .Where(x => academicPlan.Result.AcademicCoursesStrings.Contains(
                            ((int)Enum.Parse(typeof(Semesters), x.Semester) / 2 + (int)Enum.Parse(typeof(Semesters), x.Semester) % 2).ToString()))
                        .ToList();
                    tableBody = _serviceSP.GetPropertiesValues(academicPlanRecords.Result.List, names.propertiesNames);
                }
            }
            ViewBag.menuElement = defaultMenu;
            return PartialView("../StudyProcess/Table", (tableHead, tableBody, actions, Id));
        }
    }
}
