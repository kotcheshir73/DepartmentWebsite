using BaseInterfaces.ViewModels;
using ScheduleInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentWebCore.Models
{
	public class ScheduleStudentGroupsModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        public List<ScheduleRecordViewModel> List { get; set; }

        public List<StudentGroupViewModel> StudentGroups { get; set; }
    }
}