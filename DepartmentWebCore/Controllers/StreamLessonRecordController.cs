using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using DepartmentWebCore.Models;
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
    public class StreamLessonRecordController : Controller
    {
        private readonly IStreamLessonRecordService _serviceSLR;

        private readonly IStreamLessonService _serviceSL;

        private static IStudyProcessService _serviceSP;

        private const string defaultMenu = "StreamLessonRecord";

        public StreamLessonRecordController(IStreamLessonRecordService serviceSLR, IStreamLessonService serviceSL,
            IStudyProcessService serviceSP)
        {
            _serviceSLR = serviceSLR;
            _serviceSL = serviceSL;
            _serviceSP = serviceSP;
        }

        public IActionResult View(Guid Id)
        {
            var streamLessonRecord = _serviceSLR.GetStreamLessonRecord(new StreamLessonRecordGetBindingModel { Id = Id });
            if (streamLessonRecord.Succeeded)
            {
                var streamLesson = _serviceSL.GetStreamLesson(new StreamLessonGetBindingModel { Id = streamLessonRecord.Result.StreamLessonId });
                if (streamLesson.Succeeded)
                {
                    var streamLessons = _serviceSL.GetStreamLessons(new StreamLessonGetBindingModel { AcademicYearId = streamLesson.Result.AcademicYearId });
                    var academicPlans = _serviceSLR.GetAcademicPlans(new AcademicPlanGetBindingModel { AcademicYearId = streamLesson.Result.AcademicYearId });
                    if (streamLessons.Succeeded && academicPlans.Succeeded)
                    {
                        ViewBag.StreamLessons = new SelectList(streamLessons.Result.List, "Id", "StreamLessonName");
                        ViewBag.AcademicPlans = academicPlans.Result.List.Select(x => new SelectListItem()
                        {
                            Value = x.Id.ToString(),
                            Text = x.ToString()
                        });

                        ViewBag.menuElement = defaultMenu;
                        return View("../StudyProcess/StreamLessonRecord", streamLessonRecord.Result);
                    }
                }
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Create(Guid Id)
        {
            var streamLessonRecordView = new StreamLessonRecordViewModel() { StreamLessonId = Id };
            var streamLesson = _serviceSL.GetStreamLesson(new StreamLessonGetBindingModel { Id = Id });
            if (streamLesson.Succeeded)
            {
                var streamLessons = _serviceSL.GetStreamLessons(new StreamLessonGetBindingModel { AcademicYearId = streamLesson.Result.AcademicYearId });
                var academicPlans = _serviceSLR.GetAcademicPlans(new AcademicPlanGetBindingModel { AcademicYearId = streamLesson.Result.AcademicYearId });
                if (streamLessons.Succeeded && academicPlans.Succeeded)
                {
                    ViewBag.StreamLessons = new SelectList(streamLessons.Result.List, "Id", "StreamLessonName");
                    ViewBag.AcademicPlans = academicPlans.Result.List.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.ToString()
                    });

                    ViewBag.menuElement = defaultMenu;
                    return View("../StudyProcess/StreamLessonRecord", streamLessonRecordView);
                }
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public IActionResult Delete(Guid Id)
        {
            var result = _serviceSLR.DeleteStreamLessonRecord(new StreamLessonRecordGetBindingModel { Id = Id });

            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }
            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult Save(StreamLessonRecordSetBindingModel model)
        {
            ResultService result;
            string error = string.Empty;

            if (model?.Id == Guid.Empty)
            {
                result = _serviceSLR.CreateStreamLessonRecord(new StreamLessonRecordSetBindingModel
                {
                    StreamLessonId = model.StreamLessonId,
                    AcademicPlanRecordElementId = model.AcademicPlanRecordElementId,
                    IsMain = model.IsMain
                });
            }
            else
            {
                result = _serviceSLR.UpdateStreamLessonRecord(new StreamLessonRecordSetBindingModel
                {
                    Id = model.Id,
                    StreamLessonId = model.StreamLessonId,
                    AcademicPlanRecordElementId = model.AcademicPlanRecordElementId,
                    IsMain = model.IsMain
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
            var names = _serviceSP.GetPropertiesNames(typeof(StreamLessonRecordViewModel));
            var tableHead = names.displayNames;

            List<List<object>> tableBody = new List<List<object>>();
            List<(bool IsDropdownElement, MenuElementModel action)> actions = new List<(bool IsDropdownElement, MenuElementModel action)>();

            if (Id != Guid.Empty)
            {
                var streamLessonRecords = _serviceSLR.GetStreamLessonRecords(new StreamLessonRecordGetBindingModel { StreamLessonId = Id });
                if (streamLessonRecords.Succeeded)
                {
                    tableBody = _serviceSP.GetPropertiesValues(streamLessonRecords.Result.List, names.propertiesNames);
                }
            }

            ViewBag.menuElement = defaultMenu;
            return PartialView("../StudyProcess/Table", (tableHead, tableBody, actions, Id));
        }

        [HttpPost]
        public IActionResult GetAcademicPlanRecords(Guid AcademicPlanId)
        {
            var academicPlanRecords = _serviceSLR.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { AcademicPlanId = AcademicPlanId });
            if (academicPlanRecords.Succeeded)
            {
                return Json(academicPlanRecords.Result.List);
            }
            return Json(null);
        }

        [HttpPost]
        public IActionResult GetAcademicPlanRecordElements(Guid AcademicPlanRecordId)
        {
            var academicPlanRecordElements = _serviceSLR.GetAcademicPlanRecordElements(new AcademicPlanRecordElementGetBindingModel { AcademicPlanRecordId = AcademicPlanRecordId });
            if (academicPlanRecordElements.Succeeded)
            {
                return Json(academicPlanRecordElements.Result.List);
            }
            return Json(null);
        }
    }
}
