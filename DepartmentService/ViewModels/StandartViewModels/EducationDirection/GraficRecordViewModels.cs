using System;

namespace DepartmentService.ViewModels
{
    public class GraficRecordPageViewModel : PageViewModel<GraficRecordViewModel> { }

    public class GraficRecordViewModel
    {
        public Guid Id { get; set; }

        public Guid GraficId { get; set; }

        public Guid TimeNormId { get; set; }

        public int WeekNumber { get; set; }

        public double Hours { get; set; }

        public string TimeNormName { get; set; }

        public string TimeNormHours { get; set; }
    }
}
