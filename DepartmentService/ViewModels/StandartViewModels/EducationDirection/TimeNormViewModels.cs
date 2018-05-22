using System;

namespace DepartmentService.ViewModels
{
	public class TimeNormPageViewModel : PageViewModel<TimeNormViewModel> { }

	public class TimeNormViewModel
	{
		public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid DisciplineBlockId { get; set; }

        public string AcademicYear { get; set; }

        public string DisciplineBlockName { get; set; }

        public string TimeNormName { get; set; }

        public string TimeNormShortName { get; set; }

        public int TimeNormOrder { get; set; }

        public string TimeNormAcademicLevel { get; set; }

        public string KindOfLoadName { get; set; }

        public string KindOfLoadAttributeName { get; set; }

        public string KindOfLoadBlueAsteriskName { get; set; }

        public string KindOfLoadBlueAsteriskAttributeName { get; set; }

        public string KindOfLoadBlueAsteriskPracticName { get; set; }

        public string KindOfLoadType { get; set; }

		public decimal? Hours { get; set; }

        public decimal? NumKoef { get; set; }

        public string TimeNormKoef { get; set; }
    }
}
