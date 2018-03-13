using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.ViewModels
{
    public class AcademicPlanRecordElementPageViewModel : PageViewModel<AcademicPlanRecordElementViewModel> { }
    public class AcademicPlanRecordElementViewModel
    {
        public Guid Id { get; set; }
        public Guid AcademicPlanRecordId { get; set; }
        public Guid KindOfLoadId { get; set; }
        public decimal Hours { get; set; }
    }
}
