﻿using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using BaseInterfaces.BindingModels;
using DepartmentWebCore.Models;
using Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tools;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    public class AcademicPlanController : Controller
    {
        private readonly IAcademicPlanService _serviceAP;

        private static IStudyProcessService _serviceSP;

        private readonly IAcademicYearProcess _process;

        private const string defaultMenu = "AcademicPlan";

        private readonly IHostingEnvironment _hostingEnvironment;

        public AcademicPlanController(IAcademicPlanService serviceAP, IStudyProcessService serviceSP,
            IAcademicYearProcess process, IHostingEnvironment hostingEnvironment)
        {
            _serviceAP = serviceAP;
            _serviceSP = serviceSP;
            _process = process;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult View(Guid Id)
        {
            var academicPlan = _serviceAP.GetAcademicPlan(new AcademicPlanGetBindingModel { Id = Id });
            var academicYears = _serviceAP.GetAcademicYears(new AcademicYearGetBindingModel { });
            var educationDirections = _serviceAP.GetEducationDirections(new EducationDirectionGetBindingModel { });
            if (academicPlan.Succeeded && academicYears.Succeeded && educationDirections.Succeeded)
            {
                ViewBag.AcademicYears = new SelectList(academicYears.Result.List, "Id", "Title");
                ViewBag.EducationDirections = educationDirections.Result.List.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.ToString()
                });

                ViewBag.AcademicCourses = Enum.GetValues(typeof(AcademicCourse))
                    .Cast<AcademicCourse>()
                    .ToList();

                ViewBag.menuElement = defaultMenu;
                return View("../StudyProcess/AcademicPlan", academicPlan.Result);
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        public IActionResult Create(Guid Id)
        {
            var academicPlanView = new AcademicPlanViewModel() { AcademicYearId = Id, AcademicCourses = 0 };
            var academicYears = _serviceAP.GetAcademicYears(new AcademicYearGetBindingModel { });
            var educationDirections = _serviceAP.GetEducationDirections(new EducationDirectionGetBindingModel { });
            if (academicYears.Succeeded && educationDirections.Succeeded)
            {
                ViewBag.AcademicYears = new SelectList(academicYears.Result.List, "Id", "Title");
                ViewBag.EducationDirections = educationDirections.Result.List.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.ToString()
                });

                ViewBag.AcademicCourses = Enum.GetValues(typeof(AcademicCourse))
                    .Cast<AcademicCourse>()
                    .ToList();

                ViewBag.menuElement = defaultMenu;
                return View("../StudyProcess/AcademicPlan", academicPlanView);
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        [HttpPost]
        public IActionResult Delete(Guid Id)
        {
            var result = _serviceAP.DeleteAcademicPlan(new AcademicPlanGetBindingModel { Id = Id });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }
            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult Save(AcademicPlanSetBindingModel model)
        {
            ResultService result;
            string error = string.Empty;

            if (model?.Id == Guid.Empty)
            {
                result = _serviceAP.CreateAcademicPlan(new AcademicPlanSetBindingModel
                {
                    AcademicYearId = model.AcademicYearId,
                    EducationDirectionId = model.EducationDirectionId,
                    AcademicCourses = model.AcademicCourses
                });
            }
            else
            {
                result = _serviceAP.UpdateAcademicPlan(new AcademicPlanSetBindingModel
                {
                    Id = model.Id,
                    AcademicYearId = model.AcademicYearId,
                    EducationDirectionId = model.EducationDirectionId,
                    AcademicCourses = model.AcademicCourses
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
            var names = _serviceSP.GetPropertiesNames(typeof(AcademicPlanViewModel));
            var tableHead = names.displayNames;

            List<List<object>> tableBody = new List<List<object>>();
            List<(bool IsDropdownElement, MenuElementModel action)> actions = new List<(bool IsDropdownElement, MenuElementModel action)> {
                (true, new MenuElementModel
                    {
                        Name = "Создать контингент",
                        Controller = "AcademicPlan",
                        Action = "CreateContingent"
                    }
                ),
                (false, new MenuElementModel
                    {
                        Name = "Загрузить из xml",
                        Controller = "AcademicPlan",
                        Action = "LoadFromXML",
                        AdditionalParameters = new Dictionary<string, string>{ {"ButtonName", "↥"}, {"File", "plx"} }
                    }
                ),
                (true, new MenuElementModel
                    {
                        Name = "Перенос данных",
                        Controller = "AcademicPlan",
                        Action = "MakeDuplicate",
                        AdditionalParameters = new Dictionary<string, string>{ { "Select", "GetAcademicYears" }, { "Variable", "AcademicYearId" }, { "Bool", "AcademicPlan:Учебные планы,Contingent:Контингент,TimeNorm:Нормы времени" } }
                    }
                ),
            };

            if (Id != Guid.Empty)
            {
                var academicPlans = _serviceAP.GetAcademicPlans(new AcademicPlanGetBindingModel { AcademicYearId = Id });
                if (academicPlans.Succeeded)
                {
                    tableBody = _serviceSP.GetPropertiesValues(academicPlans.Result.List, names.propertiesNames);
                }
            }

            ViewBag.menuElement = defaultMenu;
            return PartialView("../StudyProcess/Table", (tableHead, tableBody, actions, Id));
        }

        [HttpPost]
        public IActionResult CreateContingent(Guid Id)
        {
            var result = _process.CreateContingentForAcademicYear(new AcademicYearGetBindingModel { Id = Id });
            string error = string.Empty;

            if (!result.Succeeded)
            {
                error = result.Errors.LastOrDefault().Value;
            }

            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult LoadFromXML(Guid Id, IFormFile file)
        {
            ResultService result;
            string error = string.Empty;

            try
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, file.FileName);

                using (FileStream output = System.IO.File.Create(path))
                    file.CopyTo(output);

                result = _process.LoadFromBlueAsteriskAcademicPlanRecord(new EducationalProcessLoadFromXMLBindingModel
                {
                    Id = Id,
                    FileName = path
                });

                if (!result.Succeeded)
                {
                    error = result.Errors.LastOrDefault().Value;
                }

                System.IO.File.Delete(path);
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            return Json(new Dictionary<string, object> { { "error", error } });
        }

        [HttpPost]
        public IActionResult GetAcademicYears(Guid Id)
        {
            var result = _serviceAP.GetAcademicYears(new AcademicYearGetBindingModel { });
            if (result.Succeeded)
            {
                return Json(new SelectList(result.Result.List.Where(x => x.Id != Id), "Id", "Title"));
            }
            return Json(null);
        }

        [HttpPost]
        public IActionResult MakeDuplicate(Guid Id, Guid AcademicYearId, bool AcademicPlan, bool Contingent, bool TimeNorm)
        {
            var result = _process.DuplicateAcademicYearElements(new EducationalProcessDuplicateAcademicYear
            {
                ToAcademicPlanId = Id,
                FromAcademicPlanId = AcademicYearId,
                DuplicateAcademicPlan = AcademicPlan,
                DuplicateContingent = Contingent,
                DuplicateTimeNorm = TimeNorm,
                DuplicateSeasonDate = false
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
