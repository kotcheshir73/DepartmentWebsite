using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class DisciplineBlockRecordGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? AcademicYearId { get; set; }

        public Guid? DisciplineBlockId { get; set; }
    }

    public class DisciplineBlockRecordSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid DisciplineBlockId { get; set; }

        public Guid? EducationDirectionId { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid TimeNormId { get; set; }

        [Required(ErrorMessage = "required")]
        public string DisciplineBlockRecordTitle { get; set; }

        [Required(ErrorMessage = "required")]
        public decimal DisciplineBlockRecordHours { get; set; }
    }
}
