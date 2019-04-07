using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
	public class AcademicPlanRecordGetBindingModel : PageSettingGetBinidingModel
	{
		public Guid? AcademicPlanId { get; set; }
	}

	public class AcademicPlanRecordSetBindingModel : PageSettingSetBinidingModel
	{
		public Guid AcademicPlanId { get; set; }

		public Guid DisciplineId { get; set; }

        public Guid? ContingentId { get; set; }

        public string Semester { get; set; }

        public int Zet { get; set; }
    }
}