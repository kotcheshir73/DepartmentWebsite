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
    public class AcademicYearProcessGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? DepartmentProcessId { get; set; }

        public Guid? UserId { get; set; }
    }

    public class AcademicYearProcessRecordBindingModel : PageSettingBinidingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid DepartmentProcessId { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid AcademicYearId { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "required")]
        public ProcessStatus Status { get; set; }

        [Required(ErrorMessage = "required")]
        public bool IsConfirmed { get; set; }
        
        public Guid? UserConfirmedId { get; set; }
        
    }
}
