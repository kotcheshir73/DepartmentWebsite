﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Mvc;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.Interfaces;
using ScheduleInterfaces.ViewModels;
using WebInterfaces.BindingModels;
using WebInterfaces.Interfaces;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Controllers
{
    public class ScheduleController : Controller
    {
        private static IWebLecturerService _serviceWL;

        private static IWebStudentGroupService _serviceWSG;

        private static IClassroomService _serviceCL;

        private static IScheduleProcess _process;

        public ScheduleController(IClassroomService serviceCL, IWebLecturerService serviceWL, IWebStudentGroupService serviceWSG, IScheduleProcess process)
        {
            _serviceCL = serviceCL;
            _serviceWL = serviceWL;
            _serviceWSG = serviceWSG;
            _process = process;
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

            var classrooms = _serviceCL.GetClassrooms(new ClassroomGetBindingModel { SkipCheck = true, NotUseInSchedule = false });
            if (classrooms.Succeeded)
            {
                model.Classrooms = classrooms.Result.List;
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
                Lecturers = new List<WebLecturerViewModel>(),
                List = new List<ScheduleRecordViewModel>()
            };
            if (!string.IsNullOrEmpty(dateString))
            {
                model.Date = Convert.ToDateTime(dateString);
            }

            var lecturers = _serviceWL.GetLecturers(new WebLecturerGetBindingModel());
            if (lecturers.Succeeded)
            {
                model.Lecturers = lecturers.Result.List;
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
                StudentGroups = new List<WebStudentGroupViewModel>(),
                List = new List<ScheduleRecordViewModel>()
            };
            if (!string.IsNullOrEmpty(dateString))
            {
                model.Date = Convert.ToDateTime(dateString);
            }

            var studentGroups = _serviceWSG.GetStudentGroups(new WebStudentGroupGetBindingModel());
            if (studentGroups.Succeeded)
            {
                model.StudentGroups = studentGroups.Result.List;
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