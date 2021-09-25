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
    public class ContingentController : Controller
    {
        private readonly IContingentService _serviceC;

        private static IStudyProcessService _serviceSP;

        private const string defaultMenu = "Contingent";

        public ContingentController(IContingentService serviceC, IStudyProcessService serviceSP)
        {
            _serviceC = serviceC;
            _serviceSP = serviceSP;
        }

        public IActionResult View(Guid Id)
        {
            var contingent = _serviceC.GetContingent(new ContingentGetBindingModel { Id = Id });
            var academicYears = _serviceC.GetAcademicYears(new AcademicYearGetBindingModel() { });
            var educationDirections = _serviceC.GetEducationDirections(new EducationDirectionGetBindingModel { });
            if (contingent.Succeeded && academicYears.Succeeded && educationDirections.Succeeded)
            {
                ViewBag.AcademicYears = new SelectList(academicYears.Result.List, "Id", "Title");
                ViewBag.EducationDirections = educationDirections.Result.List.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.ToString()
                });

                ViewBag.menuElement = defaultMenu;
                return View("../StudyProcess/Contingent", contingent.Result);
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        public IActionResult Create(Guid Id)
        {
            var contingentView = new ContingentViewModel() { AcademicYearId = Id };
            var academicYears = _serviceC.GetAcademicYears(new AcademicYearGetBindingModel() { });
            var educationDirections = _serviceC.GetEducationDirections(new EducationDirectionGetBindingModel { });
            if (educationDirections.Succeeded && academicYears.Succeeded)
            {
                ViewBag.AcademicYears = new SelectList(academicYears.Result.List, "Id", "Title");
                ViewBag.EducationDirections = educationDirections.Result.List.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.ToString()
                });

                ViewBag.menuElement = defaultMenu;
                return View("../StudyProcess/Contingent", contingentView);
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        [HttpPost]
        public IActionResult Delete(Guid Id)
        {
            var result = _serviceC.DeleteContingent(new ContingentGetBindingModel { Id = Id });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }
            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult Save(ContingentSetBindingModel model)
        {
            ResultService result;
            string error = string.Empty;

            if (model?.Id == Guid.Empty)
            {
                result = _serviceC.CreateContingent(new ContingentSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    EducationDirectionId = model.EducationDirectionId,
                    ContingentName = model.ContingentName,
                    Course = (int)Math.Pow(2.0, Convert.ToDouble(model.Course) - 1.0),
                    CountGroups = model.CountGroups,
                    CountStudents = model.CountStudents,
                    CountSubgroups = model.CountSubgroups
                });
            }
            else
            {
                result = _serviceC.UpdateContingent(new ContingentSetBindingModel
                {
                    Id = model.Id,
                    AcademicYearId = model.AcademicYearId,
                    EducationDirectionId = model.EducationDirectionId,
                    ContingentName = model.ContingentName,
                    Course = (int)Math.Pow(2.0, Convert.ToDouble(model.Course) - 1.0),
                    CountGroups = model.CountGroups,
                    CountStudents = model.CountStudents,
                    CountSubgroups = model.CountSubgroups
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
            var names = _serviceSP.GetPropertiesNames(typeof(ContingentViewModel));
            var tableHead = names.displayNames;

            List<List<object>> tableBody = new List<List<object>>();
            List<(bool IsDropdownElement, MenuElementModel action)> actions = new List<(bool IsDropdownElement, MenuElementModel action)>();

            if (Id != Guid.Empty)
            {
                var contingents = _serviceC.GetContingents(new ContingentGetBindingModel { AcademicYearId = Id });
                if (contingents.Succeeded)
                {
                    tableBody = _serviceSP.GetPropertiesValues(contingents.Result.List, names.propertiesNames);
                }
            }

            ViewBag.menuElement = defaultMenu;
            return PartialView("../StudyProcess/Table", (tableHead, tableBody, actions, Id));
        }
    }
}
