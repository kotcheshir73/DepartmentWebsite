using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentProcessAccountingService.BindingModels
{
    public class ProcessDirectionRecordGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DepartmentProcessId { get; set; }
    }

    public class ProcessDirectionRecordRecordBindingModel : PageSettingBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid DepartmentProcessId { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid EducationDirectionId { get; set; }

        [Required(ErrorMessage = "required")]
        public AcademicCourse AcademicCourse  { get; set; }

    }
}
