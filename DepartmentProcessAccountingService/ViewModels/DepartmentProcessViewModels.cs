using DepartmentModel.Enums;
using DepartmentProcessAccountingModel.Enums;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentProcessAccountingService.ViewModels
{
    public class DepartmentProcessPageViewModel : PageViewModel<DepartmentProcessViewModel> { }

    public class DepartmentProcessViewModel
    {
        public Guid Id { get; set; }

        public Post Executor { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateFinish { get; set; }

        public SemesterDates? SemesterDateStart { get; set; }

        public int? SemesterDateStartIndent { get; set; }

        public TimeDuration? Periodicity { get; set; }

        public SemesterDates? SemesterDateFinish { get; set; }

        public int? SemesterDateFinishIndent { get; set; }

        public bool Confirmability { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
