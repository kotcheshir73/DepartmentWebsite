using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace LearningProgressInterfaces.BindingModels
{
    public class DisciplineStudentRecordGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? DisciplineId { get; set; }

        public Guid? StudentGroupId { get; set; }

        public Guid? StudentId { get; set; }

        public string Semester { get; set; }
    }

    public class DisciplineStudentRecordSetBindingModel : PageSettingSetBinidingModel
    {
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