﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class DisciplineLessonGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DisciplineId { get; set; }

        public string LessonType { get; set; }
    }

    public class DisciplineLessonRecordBindingModel : PageSettingBinidingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid DisciplineId { get; set; }

        public string Disciplne { get; set; }

        [Required(ErrorMessage = "required")]
        public string LessonType { get; set; }

        [Required(ErrorMessage = "required")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "required")]
        public int Order { get; set; }

        [Required(ErrorMessage = "required")]
        public int CountOfPairs { get; set; }

        public DateTime? Date { get; set; }

        public byte[] DisciplineLessonFile { get; set; }

    }
}