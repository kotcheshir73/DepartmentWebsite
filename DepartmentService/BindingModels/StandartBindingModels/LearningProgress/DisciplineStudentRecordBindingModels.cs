using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class DisciplineStudentRecordGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DisciplineId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public Guid? StudentId { get; set; }

        public string Semester { get; set; }
    }

    public class DisciplineStudentRecordSetBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid DisciplineId { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "required")]
        public string Semester { get; set; }

        [Required(ErrorMessage = "required")]
        public string Variant { get; set; }
        
        public int SubGroup { get; set; }
    }
}
