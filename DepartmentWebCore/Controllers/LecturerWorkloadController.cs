using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
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
    public class LecturerWorkloadController : Controller
    {
        private readonly ILecturerWorkloadService _serviceLW;

        private static IStudyProcessService _serviceSP;

        private readonly IAcademicYearProcess _process;

        private const string defaultMenu = "LecturerWorkload";

        public LecturerWorkloadController(ILecturerWorkloadService serviceLW, IStudyProcessService serviceSP,
            IAcademicYearProcess process)
        {
            _serviceLW = serviceLW;
            _serviceSP = serviceSP;
            _process = process;
        }

        public IActionResult View(Guid Id)
        {
            var lecturerWorkload = _serviceLW.GetLecturerWorkload(new LecturerWorkloadGetBindingModel { Id = Id });
            var academicYears = _serviceLW.GetAcademicYears(new AcademicYearGetBindingModel() { });
            var lecturers = _serviceLW.GetLecturers(new LecturerGetBindingModel { });
            if (lecturerWorkload.Succeeded && academicYears.Succeeded && lecturers.Succeeded)
            {
                ViewBag.AcademicYears = new SelectList(academicYears.Result.List, "Id", "Title");
                ViewBag.Lecturers = new SelectList(lecturers.Result.List.OrderBy(x => x.LastName), "Id", "FullName");

                ViewBag.menuElement = defaultMenu;
                return View("../StudyProcess/LecturerWorkload", lecturerWorkload.Result);
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        public IActionResult Create(Guid Id)
        {
            var lecturerWorkloadView = new LecturerWorkloadViewModel() { AcademicYearId = Id };
            var academicYears = _serviceLW.GetAcademicYears(new AcademicYearGetBindingModel() { });
            var lecturers = _serviceLW.GetLecturers(new LecturerGetBindingModel { });
            if (lecturers.Succeeded && academicYears.Succeeded)
            {
                ViewBag.AcademicYears = new SelectList(academicYears.Result.List, "Id", "Title");
                ViewBag.Lecturers = new SelectList(lecturers.Result.List.OrderBy(x => x.LastName), "Id", "FullName");

                ViewBag.menuElement = defaultMenu;
                return View("../StudyProcess/LecturerWorkload", lecturerWorkloadView);
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        [HttpPost]
        public IActionResult Delete(Guid Id)
        {
            var result = _serviceLW.DeleteLecturerWorkload(new LecturerWorkloadGetBindingModel { Id = Id });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }
            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult Save(LecturerWorkloadSetBindingModel model, string Workload)
        {
            ResultService result;
            string error = string.Empty;

            if (model?.Id == Guid.Empty)
            {
                result = _serviceLW.CreateLecturerWorkload(new LecturerWorkloadSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    LecturerId = model.LecturerId,
                    Workload = double.Parse(Workload.Replace('.', ','))
                });
            }
            else
            {
                result = _serviceLW.UpdateLecturerWorkload(new LecturerWorkloadSetBindingModel
                {
                    Id = model.Id,
                    AcademicYearId = model.AcademicYearId,
                    LecturerId = model.LecturerId,
                    Workload = double.Parse(Workload.Replace('.', ','))
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
            var names = _serviceSP.GetPropertiesNames(typeof(LecturerWorkloadViewModel));
            var tableHead = names.displayNames;

            List<List<object>> tableBody = new List<List<object>>();
            List<(bool IsDropdownElement, MenuElementModel action)> actions = new List<(bool IsDropdownElement, MenuElementModel action)>
            {
                (true, new MenuElementModel
                    {
                        Name = "Создать нагрузку",
                        Controller = "LecturerWorkloadController",
                        Action = "CreateLecturerWorkloads"
                    }
                )
            };

            if (Id != Guid.Empty)
            {
                var lecturerWorkloads = _serviceLW.GetLecturerWorkloads(new LecturerWorkloadGetBindingModel { AcademicYearId = Id });
                if (lecturerWorkloads.Succeeded)
                {
                    tableBody = _serviceSP.GetPropertiesValues(lecturerWorkloads.Result.List.OrderBy(x => x.Lecturer).ToList(), names.propertiesNames);
                }
            }

            ViewBag.menuElement = defaultMenu;
            return PartialView("../StudyProcess/Table", (tableHead, tableBody, actions, Id));
        }

        [HttpPost]
        public IActionResult CreateLecturerWorkloads(Guid Id)
        {
            var result = _process.CreateLecturerWorkloads(new AcademicYearGetBindingModel { Id = Id });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }

            return Json(new Dictionary<string, object> { { "error", error } });
        }
    }
}
