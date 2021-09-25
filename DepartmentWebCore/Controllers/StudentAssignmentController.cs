using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
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
    public class StudentAssignmentController : Controller
    {
        private readonly IStudentAssignmentService _serviceSA;

        private static IStudyProcessService _serviceSP;

        private const string defaultMenu = "StudentAssignment";

        public StudentAssignmentController(IStudentAssignmentService serviceSA, IStudyProcessService serviceSP)
        {
            _serviceSA = serviceSA;
            _serviceSP = serviceSP;
        }

        public IActionResult View(Guid Id)
        {
            var studentAssignment = _serviceSA.GetStudentAssignment(new StudentAssignmentGetBindingModel { Id = Id });
            var academicYears = _serviceSA.GetAcademicYears(new AcademicYearGetBindingModel() { });
            var educationDirections = _serviceSA.GetEducationDirections(new EducationDirectionGetBindingModel { });
            var lecturers = _serviceSA.GetLecturers(new LecturerGetBindingModel { });
            if (studentAssignment.Succeeded && academicYears.Succeeded && educationDirections.Succeeded && lecturers.Succeeded)
            {
                ViewBag.AcademicYears = new SelectList(academicYears.Result.List, "Id", "Title");
                ViewBag.EducationDirections = educationDirections.Result.List.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.ToString()
                });
                ViewBag.Lecturers = new SelectList(lecturers.Result.List.OrderBy(x => x.LastName), "Id", "FullName");

                ViewBag.menuElement = defaultMenu;
                return View("../StudyProcess/StudentAssignment", studentAssignment.Result);
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        public IActionResult Create(Guid Id)
        {
            var studentAssignmentView = new StudentAssignmentViewModel() { AcademicYearId = Id };
            var academicYears = _serviceSA.GetAcademicYears(new AcademicYearGetBindingModel() { });
            var educationDirections = _serviceSA.GetEducationDirections(new EducationDirectionGetBindingModel { });
            var lecturers = _serviceSA.GetLecturers(new LecturerGetBindingModel { });
            if (academicYears.Succeeded && educationDirections.Succeeded && lecturers.Succeeded)
            {
                ViewBag.AcademicYears = new SelectList(academicYears.Result.List, "Id", "Title");
                ViewBag.EducationDirections = educationDirections.Result.List.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.ToString()
                });
                ViewBag.Lecturers = new SelectList(lecturers.Result.List.OrderBy(x => x.LastName), "Id", "FullName");

                ViewBag.menuElement = defaultMenu;
                return View("../StudyProcess/StudentAssignment", studentAssignmentView);
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        [HttpPost]
        public IActionResult Delete(Guid Id)
        {
            var result = _serviceSA.DeleteStudentAssignment(new StudentAssignmentGetBindingModel { Id = Id });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }
            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult Save(StudentAssignmentSetBindingModel model)
        {
            ResultService result;
            string error = string.Empty;

            if (model?.Id == Guid.Empty)
            {
                result = _serviceSA.CreateStudentAssignment(new StudentAssignmentSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    EducationDirectionId = model.EducationDirectionId,
                    LecturerId = model.LecturerId,
                    CountStudents = model.CountStudents
                });
            }
            else
            {
                result = _serviceSA.UpdateStudentAssignment(new StudentAssignmentSetBindingModel
                {
                    Id = model.Id,
                    AcademicYearId = model.AcademicYearId,
                    EducationDirectionId = model.EducationDirectionId,
                    LecturerId = model.LecturerId,
                    CountStudents = model.CountStudents
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
            var names = _serviceSP.GetPropertiesNames(typeof(StudentAssignmentViewModel));
            var tableHead = names.displayNames;

            List<List<object>> tableBody = new List<List<object>>();
            List<(bool IsDropdownElement, MenuElementModel action)> actions = new List<(bool IsDropdownElement, MenuElementModel action)>();

            if (Id != Guid.Empty)
            {
                var studentAssignments = _serviceSA.GetStudentAssignments(new StudentAssignmentGetBindingModel { AcademicYearId = Id });
                if (studentAssignments.Succeeded)
                {
                    tableBody = _serviceSP.GetPropertiesValues(studentAssignments.Result.List, names.propertiesNames);
                }
            }

            ViewBag.menuElement = defaultMenu;
            return PartialView("../StudyProcess/Table", (tableHead, tableBody, actions, Id));
        }
    }
}
