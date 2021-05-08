using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    public class AcademicPlanRecordElementController : Controller
    {
        private readonly IAcademicPlanRecordService _serviceAPR;

        private readonly IAcademicPlanRecordElementService _serviceAPRE;

        private readonly IAcademicYearProcess _process;

        private static IStudyProcessService _serviceSP;

        private const string defaultMenu = "AcademicPlanRecordElement";

        public AcademicPlanRecordElementController(IAcademicPlanRecordService serviceAPR, IAcademicPlanRecordElementService serviceAPRE,
            IStudyProcessService serviceSP, IAcademicYearProcess process)
        {
            _serviceAPR = serviceAPR;
            _serviceAPRE = serviceAPRE;
            _serviceSP = serviceSP;
            _process = process;
        }

        public IActionResult View(Guid Id)
        {
            var academicPlanRecordElement = _serviceAPRE.GetAcademicPlanRecordElement(new AcademicPlanRecordElementGetBindingModel { Id = Id });
            if (academicPlanRecordElement.Succeeded)
            {
                var academicPlanRecord = _serviceAPR.GetAcademicPlanRecord(new AcademicPlanRecordGetBindingModel { Id = academicPlanRecordElement.Result.AcademicPlanRecordId });
                var timeNorms = _serviceAPRE.GetTimeNorms(new TimeNormGetBindingModel { AcademicPlanRecordId = academicPlanRecordElement.Result.AcademicPlanRecordId });
                if (academicPlanRecord.Succeeded && timeNorms.Succeeded)
                {
                    var academicPlanRecords = _serviceAPR.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { AcademicPlanId = academicPlanRecord.Result.AcademicPlanId });
                    if (academicPlanRecords.Succeeded)
                    {
                        ViewBag.AcademicPlanRecords = new SelectList(academicPlanRecords.Result.List, "Id", "Discipline");
                        ViewBag.TimeNorms = new SelectList(timeNorms.Result.List, "Id", "KindOfLoadName");

                        ViewBag.menuElement = defaultMenu;
                        return View("../StudyProcess/AcademicPlanRecordElement", academicPlanRecordElement.Result);
                    }
                }
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Create(Guid Id)
        {
            var academicPlanRecordElementView = new AcademicPlanRecordElementViewModel() { AcademicPlanRecordId = Id };
            var academicPlanRecord = _serviceAPR.GetAcademicPlanRecord(new AcademicPlanRecordGetBindingModel { Id = Id });
            var timeNorms = _serviceAPRE.GetTimeNorms(new TimeNormGetBindingModel { AcademicPlanRecordId = Id });
            if (academicPlanRecord.Succeeded && timeNorms.Succeeded)
            {
                var academicPlanRecords = _serviceAPR.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { AcademicPlanId = academicPlanRecord.Result.AcademicPlanId });
                if (academicPlanRecords.Succeeded)
                {
                    ViewBag.AcademicPlanRecords = new SelectList(academicPlanRecords.Result.List, "Id", "Discipline");
                    ViewBag.TimeNorms = new SelectList(timeNorms.Result.List, "Id", "KindOfLoadName");

                    ViewBag.menuElement = defaultMenu;
                    return View("../StudyProcess/AcademicPlanRecordElement", academicPlanRecordElementView);
                }
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public IActionResult Delete(Guid Id)
        {
            var result = _serviceAPRE.DeleteAcademicPlanRecordElement(new AcademicPlanRecordElementGetBindingModel { Id = Id });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }
            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult Save(AcademicPlanRecordElementSetBindingModel model, string PlanHours, string FactHours)
        {
            ResultService result;
            string error = string.Empty;

            if (model?.Id == Guid.Empty)
            {
                result = _serviceAPRE.CreateAcademicPlanRecordElement(new AcademicPlanRecordElementSetBindingModel
                {
                    AcademicPlanRecordId = model.AcademicPlanRecordId,
                    TimeNormId = model.TimeNormId,
                    PlanHours = decimal.Parse(PlanHours.Replace('.', ',')),
                    FactHours = decimal.Parse(FactHours.Replace('.', ',')),
                });
            }
            else
            {
                result = _serviceAPRE.UpdateAcademicPlanRecordElement(new AcademicPlanRecordElementSetBindingModel
                {
                    Id = model.Id,
                    AcademicPlanRecordId = model.AcademicPlanRecordId,
                    TimeNormId = model.TimeNormId,
                    PlanHours = decimal.Parse(PlanHours.Replace('.', ',')),
                    FactHours = decimal.Parse(FactHours.Replace('.', ',')),
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
            var names = _serviceSP.GetPropertiesNames(typeof(AcademicPlanRecordElementViewModel));
            var tableHead = names.displayNames;

            List<List<object>> tableBody = new List<List<object>>();
            List<(bool IsDropdownElement, MenuElementModel action)> actions = new List<(bool IsDropdownElement, MenuElementModel action)> {
                (false, new MenuElementModel
                    {
                        Name = "Перенести в другую нагрузку",
                        Controller = "AcademicPlanRecordElement",
                        Action = "ChangeAcademicPlanRecord",
                        AdditionalParameters = new Dictionary<string, string>{ {"ButtonName", " ↱"}, {"Select", "GetOtherAcademicPlanRecords"}, { "Variable", "AcademicPlanRecordId" } }
                    }
                )
            };

            if (Id != Guid.Empty)
            {
                var academicPlanRecordElements = _serviceAPRE.GetAcademicPlanRecordElements(new AcademicPlanRecordElementGetBindingModel { AcademicPlanRecordId = Id });
                if (academicPlanRecordElements.Succeeded)
                {
                    tableBody = _serviceSP.GetPropertiesValues(academicPlanRecordElements.Result.List, names.propertiesNames);
                }
            }

            ViewBag.menuElement = defaultMenu;
            return PartialView("../StudyProcess/Table", (tableHead, tableBody, actions, Id));
        }

        [HttpPost]
        public IActionResult GetOtherAcademicPlanRecords(Guid Id)
        {
            var result = _process.GetOtherAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { Id = Id });
            if (result.Succeeded)
            {
                return Json(new SelectList(result.Result.List, "Id", "Discipline"));
            }
            return Json(null);
        }

        [HttpPost]
        public IActionResult ChangeAcademicPlanRecord(Guid Id, Guid AcademicPlanRecordId)
        {
            var result = _process.ChangeAPRFromAPRE(new AcademicPlanRecordElementGetBindingModel
            {
                Id = Id,
                AcademicPlanRecordId = AcademicPlanRecordId
            });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }

            return Json(new Dictionary<string, object> { { "error", error } });
        }
    }
}
