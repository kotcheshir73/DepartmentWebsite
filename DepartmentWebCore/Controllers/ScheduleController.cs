using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private static IWebClassroomService _serviceCL;

        private static IWebLecturerService _serviceWL;

        private static IWebStudentGroupService _serviceWSG;

        private static IScheduleProcess _process;

        public ScheduleController(IWebClassroomService serviceCL, IWebLecturerService serviceWL, IWebStudentGroupService serviceWSG, IScheduleProcess process)
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

        public IActionResult Classrooms(DateTime? date)
        {
            if(!date.HasValue)
            {
                date = DateTime.Now;
            }

            var model = new ScheduleClassroomsModel
            {
                Date = DateTime.Now,
                Classrooms = new List<WebClassroomViewModel>(),
                List = new List<ScheduleRecordViewModel>()
            };

            var classrooms = _serviceCL.GetClassrooms(new WebClassroomGetBindingModel());
            if (classrooms.Succeeded)
            {
                model.Classrooms = classrooms.Result.List;
            }
            var records = _process.LoadSchedule(new LoadScheduleBindingModel
            {
                BeginDate = date.Value.Date,
                EndDate = date.Value.Date.AddDays(1).AddSeconds(-1)
            });
            if(records.Succeeded)
            {
                model.List = records.Result;
            }

            return View(model);
        }
    }
}