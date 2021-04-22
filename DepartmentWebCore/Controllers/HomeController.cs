﻿using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using DepartmentWebCore.Models;
using DepartmentWebCore.Services;
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
        private readonly INewsService _serviceN;

        private readonly IStudyProcessService _serviceSP;

        private readonly BaseService _baseService;

        private readonly IAcademicYearService _serviceAY;

        private readonly IMemoryCache cache;

        public HomeController(INewsService serviceN, IStudyProcessService serviceSP, BaseService baseService,
            IMemoryCache memoryCache, IAcademicYearService serviceAY)
        {
            _serviceN = serviceN;
            _serviceSP = serviceSP;

            _baseService = baseService;

            cache = memoryCache;

            _serviceAY = serviceAY;
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
            if (!cache.TryGetValue("mainMenu", out List<MenuElementModel> mainMenu))
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
                    Controller = "Schedule",
                    Action = "Index"
                };

                var classroomList = _baseService.GetClassrooms();
                if (classroomList != null)
                {
                    MenuElementModel classroomSchedule = new MenuElementModel()
                    {
                        Name = "Аудитории",
                        Controller = "Schedule",
                        Action = "Classrooms"
                    };

                    foreach (var tmp in classroomList)
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

                var listLecturers = _baseService.GetLecturers();
                if (listLecturers != null)
                {
                    MenuElementModel lecturer = new MenuElementModel()
                    {
                        Name = "Преподаватели",
                        Controller = "Lecturer",
                        Action = "Index"
                    };

                    MenuElementModel lecturerSchedule = new MenuElementModel()
                    {
                        Name = "Преподаватели",
                        Controller = "Schedule",
                        Action = "Lecturers"
                    };

                    foreach (var tmp in listLecturers)
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

                var educationDirectionList = _baseService.GetEducationDirections();
                if (educationDirectionList != null)
                {
                    MenuElementModel educationDirection = new MenuElementModel()
                    {
                        Name = "Направления",
                        Controller = "EducationDirection",
                        Action = "Index"
                    };

                    foreach (var ed in educationDirectionList)
                    {
                        var courses = _baseService.GetCourses(ed.Id);
                        if (courses != null && courses.Count > 0)
                        {
                            MenuElementModel contingent = new MenuElementModel()
                            {
                                Name = ed.ToShortString(),
                                Controller = "EducationDirection",
                                Action = "EducationDirection",
                                Id = ed.Id
                            };

                            foreach (var course in courses)
                            {
                                contingent.Child.Add(new MenuElementModel
                                {
                                    Id = ed.Id,
                                    Name = course.Course,
                                    Controller = "EducationDirection",
                                    Action = "EducationDirection",
                                    AdditionalParameters = new Dictionary<string, string> { { "courseId", course.Id.ToString() } }
                                });
                            }

                            educationDirection.Child.Add(contingent);
                        }
                    }

                    mainMenu.Add(educationDirection);

                    cache.Set("mainMenu", mainMenu, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10)));
                }

                var listStudentGroups = _baseService.GetStudentGroups();
                if (listStudentGroups != null)
                {
                    MenuElementModel studentgroupSchedule = new MenuElementModel()
                    {
                        Name = "Группы",
                        Controller = "Schedule",
                        Action = "StudentGroups"
                    };

                    foreach (var tmp in listStudentGroups)
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

                var academicYearsList = _serviceAY.GetAcademicYears(new AcademicYearGetBindingModel() { SkipCheck = true });
                if (academicYearsList.Succeeded)
                {
                    MenuElementModel academicYears = new MenuElementModel()
                    {
                        Name = "Учебный процесс",
                        Controller = "AcademicYear",
                        Action = "View",
                    };
                    academicYears.AdditionalParameters.Add("StudyProcess", string.Empty);

                    foreach (var tmp in academicYearsList.Result.List)
                    {
                        academicYears.Child.Add(new MenuElementModel()
                        {
                            Id = tmp.Id,
                            Name = tmp.Title,
                            Controller = "AcademicYear",
                            Action = "View"
                        });
                    }

                    academicYears.Child.Add(new MenuElementModel()
                    {
                        Id = null,
                        Name = "+",
                        Controller = "AcademicYear",
                        Action = "Create"
                    });

                    mainMenu.Add(academicYears);
                }
            }
            else
            {
                var academicYearsList = _serviceAY.GetAcademicYears(new AcademicYearGetBindingModel() { SkipCheck = true });
                if (academicYearsList.Succeeded)
                {
                    List<MenuElementModel> academicYearsChild = new List<MenuElementModel>();

                    foreach (var tmp in academicYearsList.Result.List)
                    {
                        academicYearsChild.Add(new MenuElementModel()
                        {
                            Id = tmp.Id,
                            Name = tmp.Title,
                            Controller = "AcademicYear",
                            Action = "View"
                        });
                    }

                    academicYearsChild.Add(new MenuElementModel()
                    {
                        Id = null,
                        Name = "+",
                        Controller = "AcademicYear",
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