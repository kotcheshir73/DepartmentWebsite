using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.BindingModels
{
    public class AcademicPlanRecordElementGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }
        public Guid? AcademicPlanRecordId { get; set; }
        public Guid? KindOfLoadId { get; set; }
    }
    public class AcademicPlanRecordElementRecordBindingModel
    {
        public Guid Id { get; set; }

        public Guid AcademicPlanRecordId { get; set; }

        public Guid KindOfLoadId { get; set; }

        public decimal Hours { get; set; }
    }
}
