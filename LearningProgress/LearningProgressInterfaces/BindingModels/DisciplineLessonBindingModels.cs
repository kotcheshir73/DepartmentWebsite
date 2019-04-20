using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace LearningProgressInterfaces.BindingModels
{
    public class DisciplineLessonGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicYearId { get; set; }

        public Guid? DisciplineId { get; set; }

        public Guid? EducationDirectionId { get; set; }

        public Guid? TimeNormId { get; set; }
    }

    public class DisciplineLessonRecordBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public Guid AcademicYearId { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid DisciplineId { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid EducationDirectionId { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid TimeNormId { get; set; }

        [Required(ErrorMessage = "required")]
        public string Semester { get; set; }

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