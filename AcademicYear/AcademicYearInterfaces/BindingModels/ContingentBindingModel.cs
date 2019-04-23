using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
	public class ContingentGetBindingModel : PageSettingGetBinidingModel
	{
        public Guid? AcademicYearId { get; set; }

        public Guid? AcademicPlanId { get; set; }
    }

	public class ContingentSetBindingModel : PageSettingSetBinidingModel
	{
		public Guid AcademicYearId { get; set; }

		public Guid EducationDirectionId { get; set; }

        public string ContingentName { get; set; }

        public int Course { get; set; }

        public int CountGroups { get; set; }

        public int CountStudents { get; set; }

		public int CountSubgroups { get; set; }
	}
}