using DepartmentModel.Enums;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentProcessAccountingService.ViewModels
{
    public class ProcessDirectionRecordPageViewModel : PageViewModel<ProcessDirectionRecordViewModel> { }

    public class ProcessDirectionRecordViewModel
    {
        public Guid Id { get; set; }

        public Guid DepartmentProcessId { get; set; }

        public Guid EducationDirectionId { get; set; }

        public AcademicCourse AcademicCourse { get; set; }
    }
}
