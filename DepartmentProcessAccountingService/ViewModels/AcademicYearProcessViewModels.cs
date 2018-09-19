using DepartmentProcessAccountingModel.Enums;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentProcessAccountingService.ViewModels
{
    public class AcademicYearProcessPageViewModel : PageViewModel<AcademicYearProcessViewModel> { }

    public class AcademicYearProcessViewModel
    {
        public Guid Id { get; set; }

        public Guid DepartmentProcessId { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid UserId { get; set; }

        public ProcessStatus Status { get; set; }

        public bool IsConfirmed { get; set; }

        public Guid? UserConfirmedId { get; set; }
    }
}
