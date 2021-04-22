using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
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
    public class StreamLessonController : Controller
    {
        private readonly IStreamLessonService _serviceSL;

        private readonly IAcademicYearService _serviceAY;

        private static IStudyProcessService _serviceSP;

        private readonly IAcademicYearProcess _process;

        private const string defaultMenu = "StreamLesson";

        public StreamLessonController(IStreamLessonService serviceSL, IStudyProcessService serviceSP,
            IAcademicYearService serviceAY, IAcademicYearProcess process)
        {
            _serviceSL = serviceSL;
            _serviceSP = serviceSP;
            _serviceAY = serviceAY;
            _process = process;
        }

        public IActionResult View(Guid Id)
        {
            var streamLesson = _serviceSL.GetStreamLesson(new StreamLessonGetBindingModel { Id = Id });
            var academicYears = _serviceAY.GetAcademicYears(new AcademicYearGetBindingModel() { });
            if (streamLesson.Succeeded && academicYears.Succeeded)
            {
                var semesters = Enum.GetValues(typeof(Semesters))
                    .Cast<Semesters>()
                    .Select(v => v.ToString())
                    .ToList();

                ViewBag.AcademicYears = new SelectList(academicYears.Result.List, "Id", "Title");
                ViewBag.Semesters = new SelectList(semesters);

                ViewBag.menuElement = defaultMenu;
                return View("../StudyProcess/StreamLesson", streamLesson.Result);
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        public IActionResult Create(Guid Id)
        {
            var streamLessonView = new StreamLessonViewModel() { AcademicYearId = Id };
            var academicYears = _serviceAY.GetAcademicYears(new AcademicYearGetBindingModel() { });
            if (academicYears.Succeeded)
            {
                var semesters = Enum.GetValues(typeof(Semesters))
                    .Cast<Semesters>()
                    .Select(v => v.ToString())
                    .ToList();

                ViewBag.AcademicYears = new SelectList(academicYears.Result.List, "Id", "Title");
                ViewBag.Semesters = new SelectList(semesters);

                ViewBag.menuElement = defaultMenu;
                return View("../StudyProcess/StreamLesson", streamLessonView);
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        [HttpPost]
        public IActionResult Delete(Guid Id)
        {
            var result = _serviceSL.DeleteStreamLesson(new StreamLessonGetBindingModel { Id = Id });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }
            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult Save(StreamLessonSetBindingModel model, string StreamLessonHours)
        {
            ResultService result;
            string error = string.Empty;

            if (model?.Id == Guid.Empty)
            {
                result = _serviceSL.CreateStreamLesson(new StreamLessonSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    StreamLessonName = model.StreamLessonName,
                    StreamLessonHours = decimal.Parse(StreamLessonHours.Replace('.', ',')),
                    Semester = model.Semester
                });
            }
            else
            {
                result = _serviceSL.UpdateStreamLesson(new StreamLessonSetBindingModel
                {
                    Id = model.Id,
                    AcademicYearId = model.AcademicYearId,
                    StreamLessonName = model.StreamLessonName,
                    StreamLessonHours = decimal.Parse(StreamLessonHours.Replace('.', ',')),
                    Semester = model.Semester
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
            var names = _serviceSP.GetPropertiesNames(typeof(StreamLessonViewModel));
            var tableHead = names.displayNames;

            List<List<object>> tableBody = new List<List<object>>();
            List<(bool IsDropdownElement, MenuElementModel action)> actions = new List<(bool IsDropdownElement, MenuElementModel action)> {
                (true, new MenuElementModel
                    {
                        Name = "Создать потоки",
                        Controller = "StreamLesson",
                        Action = "CreateStreams"
                    }
                )
            };

            if (Id != Guid.Empty)
            {
                var streamLessons = _serviceSL.GetStreamLessons(new StreamLessonGetBindingModel { AcademicYearId = Id });
                if (streamLessons.Succeeded)
                {
                    tableBody = _serviceSP.GetPropertiesValues(streamLessons.Result.List, names.propertiesNames);
                }
            }

            ViewBag.menuElement = defaultMenu;
            return PartialView("../StudyProcess/Table", (tableHead, tableBody, actions, Id));
        }

        [HttpPost]
        public IActionResult CreateStreams(Guid Id)
        {
            var result = _process.CreateStreamsForAcademicYear(new EducationalProcessCreateStreams { AcademicYearId = Id });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }

            return Json(new Dictionary<string, object> { { "error", error } });
        }
    }
}
