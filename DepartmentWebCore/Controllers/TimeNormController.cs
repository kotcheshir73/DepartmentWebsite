using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using DepartmentWebCore.Models;
using Enums;
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
    public class TimeNormController : Controller
    {
        private readonly ITimeNormService _serviceTN;

        private static IStudyProcessService _serviceSP;

        private const string defaultMenu = "TimeNorm";

        public TimeNormController(ITimeNormService serviceTN, IStudyProcessService serviceSP)
        {
            _serviceTN = serviceTN;
            _serviceSP = serviceSP;
        }

        public IActionResult View(Guid Id)
        {
            var timeNorm = _serviceTN.GetTimeNorm(new TimeNormGetBindingModel { Id = Id });
            var academicYears = _serviceTN.GetAcademicYears(new AcademicYearGetBindingModel() { });
            var disciplineBlocks = _serviceTN.GetDisciplineBlocks(new DisciplineBlockGetBindingModel { });
            if (timeNorm.Succeeded && academicYears.Succeeded && disciplineBlocks.Succeeded)
            {
                var educationDirectionQualifications = Enum.GetValues(typeof(EducationDirectionQualification))
                    .Cast<EducationDirectionQualification>()
                    .Select(v => v.ToString())
                    .ToList();
                educationDirectionQualifications.Insert(0, null);

                var kindOfLoadTypes = Enum.GetValues(typeof(KindOfLoadType))
                    .Cast<KindOfLoadType>()
                    .Select(v => v.ToString())
                    .ToList();

                var timeNormKoefs = Enum.GetValues(typeof(TimeNormKoef))
                    .Cast<TimeNormKoef>()
                    .Select(v => v.ToString())
                    .ToList();

                ViewBag.AcademicYears = new SelectList(academicYears.Result.List, "Id", "Title");
                ViewBag.DisciplineBlocks = new SelectList(disciplineBlocks.Result.List, "Id", "Title");
                ViewBag.EducationDirectionQualifications = new SelectList(educationDirectionQualifications);
                ViewBag.KindOfLoadTypes = new SelectList(kindOfLoadTypes);
                ViewBag.TimeNormKoefs = new SelectList(timeNormKoefs);

                ViewBag.menuElement = defaultMenu;
                return View("../StudyProcess/TimeNorm", timeNorm.Result);
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        public IActionResult Create(Guid Id)
        {
            var timeNormView = new TimeNormViewModel() { AcademicYearId = Id };
            var academicYears = _serviceTN.GetAcademicYears(new AcademicYearGetBindingModel() { });
            var disciplineBlocks = _serviceTN.GetDisciplineBlocks(new DisciplineBlockGetBindingModel { });
            if (disciplineBlocks.Succeeded && academicYears.Succeeded)
            {
                var educationDirectionQualifications = Enum.GetValues(typeof(EducationDirectionQualification))
                    .Cast<EducationDirectionQualification>()
                    .Select(v => v.ToString())
                    .ToList();
                educationDirectionQualifications.Insert(0, null);

                var kindOfLoadTypes = Enum.GetValues(typeof(KindOfLoadType))
                    .Cast<KindOfLoadType>()
                    .Select(v => v.ToString())
                    .ToList();

                var timeNormKoefs = Enum.GetValues(typeof(TimeNormKoef))
                    .Cast<TimeNormKoef>()
                    .Select(v => v.ToString())
                    .ToList();

                ViewBag.AcademicYears = new SelectList(academicYears.Result.List, "Id", "Title");
                ViewBag.DisciplineBlocks = new SelectList(disciplineBlocks.Result.List, "Id", "Title");
                ViewBag.EducationDirectionQualifications = new SelectList(educationDirectionQualifications);
                ViewBag.KindOfLoadTypes = new SelectList(kindOfLoadTypes);
                ViewBag.TimeNormKoefs = new SelectList(timeNormKoefs);

                ViewBag.menuElement = defaultMenu;
                return View("../StudyProcess/TimeNorm", timeNormView);
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        [HttpPost]
        public IActionResult Delete(Guid Id)
        {
            var result = _serviceTN.DeleteTimeNorm(new TimeNormGetBindingModel { Id = Id });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }
            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult Save(TimeNormSetBindingModel model, string Hours, string NumKoef)
        {
            ResultService result;
            string error = string.Empty;

            decimal? decHours = null;
            decimal? decNumKoef = null;

            if (Hours != null)
                decHours = decimal.Parse(Hours.Replace('.', ','));
            if (NumKoef != null)
                decNumKoef = decimal.Parse(NumKoef.Replace('.', ','));


            if (model?.Id == Guid.Empty)
            {
                result = _serviceTN.CreateTimeNorm(new TimeNormSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    DisciplineBlockId = model.DisciplineBlockId,
                    TimeNormName = model.TimeNormName,
                    TimeNormShortName = model.TimeNormShortName,
                    TimeNormOrder = model.TimeNormOrder,
                    TimeNormEducationDirectionQualification = model.TimeNormEducationDirectionQualification,
                    KindOfLoadName = model.KindOfLoadName,
                    KindOfLoadAttributeName = model.KindOfLoadAttributeName,
                    KindOfLoadBlueAsteriskName = model.KindOfLoadBlueAsteriskName,
                    KindOfLoadBlueAsteriskAttributeName = model.KindOfLoadBlueAsteriskAttributeName,
                    KindOfLoadBlueAsteriskPracticName = model.KindOfLoadBlueAsteriskPracticName,
                    KindOfLoadType = model.KindOfLoadType,
                    Hours = decHours,
                    NumKoef = decNumKoef,
                    TimeNormKoef = model.TimeNormKoef,
                    UseInLearningProgress = model.UseInLearningProgress,
                    UseInSite = model.UseInSite,
                    IsAssignmentByAdviser = model.IsAssignmentByAdviser
                });
            }
            else
            {
                result = _serviceTN.UpdateTimeNorm(new TimeNormSetBindingModel
                {
                    Id = model.Id,
                    AcademicYearId = model.AcademicYearId,
                    DisciplineBlockId = model.DisciplineBlockId,
                    TimeNormName = model.TimeNormName,
                    TimeNormShortName = model.TimeNormShortName,
                    TimeNormOrder = model.TimeNormOrder,
                    TimeNormEducationDirectionQualification = model.TimeNormEducationDirectionQualification,
                    KindOfLoadName = model.KindOfLoadName,
                    KindOfLoadAttributeName = model.KindOfLoadAttributeName,
                    KindOfLoadBlueAsteriskName = model.KindOfLoadBlueAsteriskName,
                    KindOfLoadBlueAsteriskAttributeName = model.KindOfLoadBlueAsteriskAttributeName,
                    KindOfLoadBlueAsteriskPracticName = model.KindOfLoadBlueAsteriskPracticName,
                    KindOfLoadType = model.KindOfLoadType,
                    Hours = decHours,
                    NumKoef = decNumKoef,
                    TimeNormKoef = model.TimeNormKoef,
                    UseInLearningProgress = model.UseInLearningProgress,
                    UseInSite = model.UseInSite,
                    IsAssignmentByAdviser = model.IsAssignmentByAdviser
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
            var names = _serviceSP.GetPropertiesNames(typeof(TimeNormViewModel));
            var tableHead = names.displayNames;

            List<List<object>> tableBody = new List<List<object>>();
            List<(bool IsDropdownElement, MenuElementModel action)> actions = new List<(bool IsDropdownElement, MenuElementModel action)>();

            if (Id != Guid.Empty)
            {
                var timeNorms = _serviceTN.GetTimeNorms(new TimeNormGetBindingModel { AcademicYearId = Id });
                if (timeNorms.Succeeded)
                {
                    tableBody = _serviceSP.GetPropertiesValues(timeNorms.Result.List, names.propertiesNames);
                }
            }

            ViewBag.menuElement = defaultMenu;
            return PartialView("../StudyProcess/Table", (tableHead, tableBody, actions, Id));
        }
    }
}
