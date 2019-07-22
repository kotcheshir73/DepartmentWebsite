using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    public class HomeController : Controller
    {
        private static IWebLecturerService _serviceWL;

        private static IWebEducationDirectionService _serviceWED;

        private static INewsService _serviceN;

        public HomeController(IWebLecturerService serviceWL, IWebEducationDirectionService serviceWED, INewsService serviceN)
        {
            _serviceWL = serviceWL;
            _serviceWED = serviceWED;
            _serviceN = serviceN;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NIR()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult MainMenu()
        {
            List<MenuElementModel> mainMenu = new List<MenuElementModel>
            {
                new MenuElementModel
                {
                    Controller = "News",
                    Action = "Index",
                    Name = "Новости"
                }
            };

            var lecturerList = _serviceWL.GetLecturers(new WebLecturerGetBindingModel());
            if (lecturerList.Succeeded)
            {
                MenuElementModel lecturer = new MenuElementModel()
                {
                    Name = "Преподаватели",
                    Child = new List<MenuElementModel>(),
                    Controller = "Lecturer",
                    Action = "Index"
                };

                foreach (var tmp in lecturerList.Result.List)
                {
                    lecturer.Child.Add(new MenuElementModel()
                    {
                        Id = tmp.Id,
                        Name = tmp.FullName,
                        Controller = "Lecturer",
                        Action = "Lecturer"
                    });
                }

                mainMenu.Add(lecturer);
            }

            var educationDirectionList = _serviceWED.GetEducationDirections(new WebEducationDirectionGetBindingModel());
            if (educationDirectionList.Succeeded)
            {
                MenuElementModel educationDirection = new MenuElementModel()
                {
                    Name = "Направления",
                    Child = new List<MenuElementModel>(),
                    Controller = "EducationDirection",
                    Action = "Index"
                };

                foreach (var ed in educationDirectionList.Result.List)
                {
                    MenuElementModel contingent = new MenuElementModel()
                    {
                        Name = ed.ToString(),
                        Child = new List<MenuElementModel>(),
                        Controller = "EducationDirection",
                        Action = "EducationDirection",
                        Id = ed.Id
                    };

                    foreach (var course in ed.Courses)
                    {
                        contingent.Child.Add(new MenuElementModel
                        {
                            Id = ed.Id,
                            Name = course.Item2,
                            Controller = "EducationDirection",
                            Action = "EducationDirection",
                            AdditionalParameters = new Dictionary<string, string> { { "courseId", course.Item1.ToString() } }
                        });
                    }

                    educationDirection.Child.Add(contingent);
                }

                mainMenu.Add(educationDirection);
            }

            return PartialView(mainMenu);
        }

        public ActionResult FirstNews()
        {
            var newses = _serviceN.GetNewses(new NewsGetBindingModel
            {
                PageNumber = 0,
                PageSize = 5
            });

            if (!newses.Succeeded)
            {
                return new EmptyResult();
            }

            return PartialView(newses.Result);
        }

        public ActionResult Error()
        {
            var exceptionData = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if(string.IsNullOrEmpty(exceptionData?.Error?.Message))
            {
                return new EmptyResult();
            }

            var errors = Regex.Split(exceptionData?.Error?.Message, "<br />").Where(s => !string.IsNullOrEmpty(s));

            var list = errors.Select(x => x.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1]);

            return PartialView(list.ToList());
        }
    }
}