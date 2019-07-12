using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
	public class AcademicPlanRecordPageViewModel : PageSettingListViewModel<AcademicPlanRecordViewModel> { }

	public class AcademicPlanRecordViewModel : PageSettingElementViewModel
	{
		public Guid AcademicPlanId { get; set; }

		public Guid DisciplineId { get; set; }

        public Guid? ContingentId { get; set; }

        public string Disciplne { get; set; }

		public string Semester { get; set; }

        public string ContingentGroup { get; set; }

        public int Zet { get; set; }
    }
}