using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using AcademicYearInterfaces.ViewModels;
using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace DepartmentWebCore.Controllers
{
    [Authorize(Roles = Startup.StudyProcessAuthRole)]
    public class AcademicYearController : Controller
    {
        private readonly IAcademicYearService _serviceAY;

        private const string defaultMenu = "AcademicPlan";

        public AcademicYearController(IAcademicYearService serviceAY)
        {
            _serviceAY = serviceAY;
        }

        public IActionResult View(Guid Id, string menuElement)
        {
            if (Id == Guid.Empty)
            {
                Id = _serviceAY.GetAcademicYears(new AcademicYearGetBindingModel()).Result.List.LastOrDefault().Id;
            }

            var academicYear = _serviceAY.GetAcademicYear(new AcademicYearGetBindingModel { Id = Id });
            if (academicYear.Succeeded)
            {
                AcademicYearViewModel academicYearView = academicYear.Result;
                if (menuElement == null)
                {
                    ViewBag.menuElement = defaultMenu;
                }
                else
                {
                    ViewBag.menuElement = menuElement;
                }
                return View("../StudyProcess/AcademicYear", academicYearView);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Create()
        {
            var academicYearView = new AcademicYearViewModel();
            ViewBag.menuElement = defaultMenu;
            return View("../StudyProcess/AcademicYear", academicYearView);
        }

        [HttpPost]
        public IActionResult Save(AcademicYearSetBindingModel model)
        {
            ResultService result;
            string error = string.Empty;

            if (model?.Id == Guid.Empty)
            {
                result = _serviceAY.CreateAcademicYear(new AcademicYearSetBindingModel
                {
                    Title = model.Title
                });
            }
            else
            {
                result = _serviceAY.UpdateAcademicYear(new AcademicYearSetBindingModel
                {
                    Id = model.Id,
                    Title = model.Title
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

        public ActionResult Menu(Guid Id, string menuElement)
        {
            List<SubmenuModel> menu = new List<SubmenuModel> {
                new SubmenuModel
                {
                    AcademicYearId = Id,
                    Name = "Академические планы",
                    ActionName = "AcademicPlan"
                },
                new SubmenuModel
                {
                    AcademicYearId = Id,
                    Name = "Потоки",
                    ActionName = "StreamLesson"
                },
                new SubmenuModel
                {
                    AcademicYearId = Id,
                    Name = "Нормы времени",
                    ActionName = "TimeNorm"
                },
                new SubmenuModel
                {
                    AcademicYearId = Id,
                    Name = "Контингент",
                    ActionName = "Contingent"
                },
                new SubmenuModel
                {
                    AcademicYearId = Id,
                    Name = "Нагрузка преподавателей",
                    ActionName = "LecturerWorkload"
                }
            };

            menu.ForEach(x => { if (x.ActionName == menuElement) x.isActive = true; else { x.isActive = false; } });

            return PartialView("../StudyProcess/Menu", menu);
        }
    }
}
