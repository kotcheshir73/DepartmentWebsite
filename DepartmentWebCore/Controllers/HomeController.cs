using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore.Controllers
{
    public class HomeController : Controller
    {
        private static IWebLecturerService _serviceWL;

        private static IWebEducationDirectionService _serviceWED;

        private static IWebContingentService _serviceWC;

        public HomeController(IWebLecturerService serviceWL, IWebEducationDirectionService serviceWED, IWebContingentService serviceWC)
        {
            _serviceWL = serviceWL;
            _serviceWED = serviceWED;
            _serviceWC = serviceWC;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult NIR()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult MainMenu()
        {
            List<MenuElementModel> mainMenu = new List<MenuElementModel>
            {
                new MenuElementModel
                {
                    Controller = "Event",
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
                    var courses = _serviceWC.GetCourseByContingents(new WebContingentGetBindingModel { EducationDirectionId = ed.Id });

                    if (courses.Succeeded)
                    {
                        MenuElementModel contingent = new MenuElementModel()
                        {
                            Name = ed.ToString(),
                            Child = new List<MenuElementModel>(),
                            Controller = "EducationDirection",
                            Action = "EducationDirection",
                            Id = ed.Id
                        };

                        foreach (var course in courses.Result.List)
                        {
                            contingent.Child.Add(new MenuElementModel
                            {
                                Id = course.Id,
                                Name = $"Курс {course.Course}",
                                Controller = "EducationDirection",
                                Action = "Course"
                            });
                        }

                        educationDirection.Child.Add(contingent);
                    }
                }

                mainMenu.Add(educationDirection);
            }

            return View(mainMenu);
        }
    }
}