﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class StreamLessonRecordGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? SteamLessonId { get; set; }
    }

    public class StreamLessonRecordSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid StreamLessonId { get; set; }

        public Guid AcademicPlanRecordElementId { get; set; }

        [Required(ErrorMessage = "required")]
        public bool IsMain { get; set; }
    }
}