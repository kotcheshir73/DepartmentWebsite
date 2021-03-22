using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private static IClassroomService _serviceCL;

        private static IWebLecturerService _serviceWL;

        private static IWebEducationDirectionService _serviceWED;

        private static IWebStudentGroupService _serviceWSG;

        private static INewsService _serviceN;

        private static IWebStudyProcessService _serviceSP;

        private IMemoryCache cache;

        public HomeController(IClassroomService serviceCL, IWebLecturerService serviceWL, IWebEducationDirectionService serviceWED, IWebStudentGroupService serviceWSG,
            INewsService serviceN, IWebStudyProcessService serviceSP, IMemoryCache memoryCache)
        {
            _serviceCL = serviceCL;
            _serviceWL = serviceWL;
            _serviceWED = serviceWED;
            _serviceWSG = serviceWSG;
            _serviceN = serviceN;
            _serviceSP = serviceSP;

            cache = memoryCache;
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
            List<MenuElementModel> mainMenu;

            if (!cache.TryGetValue("mainMenu", out mainMenu))
            {
                mainMenu = new List<MenuElementModel>
                {
                    new MenuElementModel
                    {
                        Controller = "News",
                        Action = "Index",
                        Name = "Новости"
                    }
                };

                MenuElementModel schedule = new MenuElementModel()
                {
                    Name = "Расписание",
                    Child = new List<MenuElementModel>(),
                    Controller = "Schedule",
                    Action = "Index"
                };

                var classroomList = _serviceCL.GetClassrooms(new ClassroomGetBindingModel { SkipCheck = true, NotUseInSchedule = false });
                if (classroomList.Succeeded)
                {
                    MenuElementModel classroomSchedule = new MenuElementModel()
                    {
                        Name = "Аудитории",
                        Child = new List<MenuElementModel>(),
                        Controller = "Schedule",
                        Action = "Classrooms"
                    };

                    foreach (var tmp in classroomList.Result.List)
                    {
                        classroomSchedule.Child.Add(new MenuElementModel()
                        {
                            Id = tmp.Id,
                            Name = tmp.Number,
                            Controller = "Schedule",
                            Action = "Classroom"
                        });
                    }

                    schedule.Child.Add(classroomSchedule);
                }

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

                    MenuElementModel lecturerSchedule = new MenuElementModel()
                    {
                        Name = "Преподаватели",
                        Child = new List<MenuElementModel>(),
                        Controller = "Schedule",
                        Action = "Lecturers"
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

                        lecturerSchedule.Child.Add(new MenuElementModel()
                        {
                            Id = tmp.Id,
                            Name = tmp.FullName,
                            Controller = "Schedule",
                            Action = "Lecturer"
                        });
                    }

                    mainMenu.Add(lecturer);

                    schedule.Child.Add(lecturerSchedule);
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

                    cache.Set("mainMenu", mainMenu, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
                }

                var studentGroups = _serviceWSG.GetStudentGroups(new WebStudentGroupGetBindingModel());
                if (studentGroups.Succeeded)
                {
                    MenuElementModel studentgroupSchedule = new MenuElementModel()
                    {
                        Name = "Группы",
                        Child = new List<MenuElementModel>(),
                        Controller = "Schedule",
                        Action = "StudentGroups"
                    };

                    foreach (var tmp in studentGroups.Result.List)
                    {
                        studentgroupSchedule.Child.Add(new MenuElementModel()
                        {
                            Id = tmp.Id,
                            Name = tmp.GroupName,
                            Controller = "Schedule",
                            Action = "StudentGroup"
                        });
                    }

                    schedule.Child.Add(studentgroupSchedule);
                }

                mainMenu.Add(schedule);

                var academicYearsList = _serviceSP.GetAcademicYears(new WebAcademicYearGetBindingModel { });
                if (academicYearsList.Succeeded)
                {
                    MenuElementModel academicYears = new MenuElementModel()
                    {
                        Id = academicYearsList.Result.List.LastOrDefault().Id,
                        Name = "Учебный процесс",
                        Controller = "StudyProcess",
                        Action = "Index",
                        Child = new List<MenuElementModel>(),
                        AdditionalParameters = new Dictionary<string, string> { { "StudyProcess", "" } }
                    };

                    foreach (var tmp in academicYearsList.Result.List)
                    {
                        academicYears.Child.Add(new MenuElementModel()
                        {
                            Id = tmp.Id,
                            Name = tmp.Title,
                            Controller = "StudyProcess",
                            Action = "Index"
                        });
                    }

                    academicYears.Child.Add(new MenuElementModel()
                    {
                        Id = null,
                        Name = "+",
                        Controller = "StudyProcess",
                        Action = "Create"
                    });

                    mainMenu.Add(academicYears);
                }
            }
            else
            {
                var academicYearsList = _serviceSP.GetAcademicYears(new WebAcademicYearGetBindingModel { });
                if (academicYearsList.Succeeded)
                {
                    List<MenuElementModel> academicYearsChild = new List<MenuElementModel>();

                    foreach (var tmp in academicYearsList.Result.List)
                    {
                        academicYearsChild.Add(new MenuElementModel()
                        {
                            Id = tmp.Id,
                            Name = tmp.Title,
                            Controller = "StudyProcess",
                            Action = "Index"
                        });
                    }

                    academicYearsChild.Add(new MenuElementModel()
                    {
                        Id = null,
                        Name = "+",
                        Controller = "StudyProcess",
                        Action = "Create"
                    });

                    mainMenu.Find(x => x.Name == "Учебный процесс").Child = academicYearsChild;
                }
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

            if (string.IsNullOrEmpty(exceptionData?.Error?.Message))
            {
                return new EmptyResult();
            }

            var errors = Regex.Split(exceptionData?.Error?.Message, "<br />").Where(s => !string.IsNullOrEmpty(s));

            var list = errors.Select(x => x.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0]);

            return PartialView(list.ToList());
        }
    }
}