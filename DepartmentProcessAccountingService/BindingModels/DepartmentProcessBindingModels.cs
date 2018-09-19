using DepartmentModel.Enums;
using DepartmentProcessAccountingModel.Enums;
using DepartmentService.BindingModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentProcessAccountingService.BindingModels
{
    public class DepartmentProcessGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }
    }

    public class DepartmentProcessRecordBindingModel : PageSettingBinidingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public Post Executor { get; set; }
        
        public DateTime? DateStart { get; set; }
        
        public DateTime? DateFinish { get; set; }

        public SemesterDates? SemesterDateStart { get; set; }

        public int? SemesterDateStartIndent { get; set; }

        public TimeDuration? Periodicity { get; set; }

        public SemesterDates? SemesterDateFinish { get; set; }

        public int? SemesterDateFinishIndent { get; set; }

        public bool Confirmability { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "required")]
        public string Title { get; set; }

    }
}
