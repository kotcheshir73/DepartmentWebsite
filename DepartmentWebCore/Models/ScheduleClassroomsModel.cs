using ScheduleInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebInterfaces.ViewModels;

namespace DepartmentWebCore.Models
{
    public class ScheduleClassroomsModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        public List<ScheduleRecordViewModel> List { get; set; }

        public List<WebClassroomViewModel> Classrooms { get; set; }
    }
}