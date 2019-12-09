﻿using Enums;
using System.ComponentModel.DataAnnotations;

namespace ScheduleInterfaces.BindingModels
{
	public class SemesterRecordSetBindingModel : ScheduleSetBindingModel
    {
        [Required(ErrorMessage = "required")]
        public LessonTypes LessonType { get; set; }

        public bool IsFirstHalfSemester { get; set; }

        public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }
    }
}