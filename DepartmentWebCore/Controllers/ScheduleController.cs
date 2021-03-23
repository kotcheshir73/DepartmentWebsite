using BaseInterfaces.ViewModels;
using DepartmentWebCore.Models;
using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Mvc;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using ScheduleInterfaces.ViewModels;
using System;
using System.Collections.Generic;

namespace DepartmentWebCore.Controllers
{
	public class ScheduleController : Controller
    {
        private static IScheduleProcess _process;

        private readonly BaseService _baseService;

        public ScheduleController(IScheduleProcess process, BaseService baseService)
        {
            _process = process;

            _baseService = baseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Classrooms(string dateString)
        {
            var model = new ScheduleClassroomsModel
            {
                Date = DateTime.Now,
                Classrooms = new List<ClassroomViewModel>(),
                List = new List<ScheduleRecordViewModel>()
            };
            if (!string.IsNullOrEmpty(dateString))
            {
                model.Date = Convert.ToDateTime(dateString);
            }

            var listClassrooms = _baseService.GetClassrooms();
            if (listClassrooms != null)
            {
                model.Classrooms = listClassrooms;
            }
            var records = _process.LoadSchedule(new LoadScheduleBindingModel
            {
                BeginDate = model.Date.Date,
                EndDate = model.Date.Date.AddDays(1).AddSeconds(-1)
            });
            if(records.Succeeded)
            {
                model.List = records.Result;
            }

            return View(model);
        }

        public IActionResult Lecturers(string dateString)
        {
            var model = new ScheduleLecturersModel
            {
                Date = DateTime.Now,
                Lecturers = new List<LecturerViewModel>(),
                List = new List<ScheduleRecordViewModel>()
            };
            if (!string.IsNullOrEmpty(dateString))
            {
                model.Date = Convert.ToDateTime(dateString);
            }

            var listLecturers = _baseService.GetLecturers();
            if (listLecturers != null)
            {
                model.Lecturers = listLecturers;
            }
            var records = _process.LoadSchedule(new LoadScheduleBindingModel
            {
                BeginDate = model.Date.Date,
                EndDate = model.Date.Date.AddDays(1).AddSeconds(-1)
            });
            if (records.Succeeded)
            {
                model.List = records.Result;
            }

            return View(model);
        }

        public IActionResult StudentGroups(string dateString)
        {
            var model = new ScheduleStudentGroupsModel
            {
                Date = DateTime.Now,
                StudentGroups = new List<StudentGroupViewModel>(),
                List = new List<ScheduleRecordViewModel>()
            };
            if (!string.IsNullOrEmpty(dateString))
            {
                model.Date = Convert.ToDateTime(dateString);
            }

            var listStudentGroups = _baseService.GetStudentGroups();
            if (listStudentGroups != null)
            {
                model.StudentGroups = listStudentGroups;
            }
            var records = _process.LoadSchedule(new LoadScheduleBindingModel
            {
                BeginDate = model.Date.Date,
                EndDate = model.Date.Date.AddDays(1).AddSeconds(-1)
            });
            if (records.Succeeded)
            {
                model.List = records.Result;
            }

            return View(model);
        }
    }
}