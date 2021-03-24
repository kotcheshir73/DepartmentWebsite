using Enums;
using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
	public class AcademicPlanRecordGetBindingModel : PageSettingGetBinidingModel
	{
		public Guid? AcademicPlanId { get; set; }

		public Semesters? Semester { get; set; }
	}

	public class AcademicPlanRecordSetBindingModel : PageSettingSetBinidingModel
	{
		public Guid AcademicPlanId { get; set; }

		public Guid DisciplineId { get; set; }

        public Guid? ContingentId { get; set; }

		public bool InDepartment { get; set; }

        public string Semester { get; set; }

        public int Zet { get; set; }

        public Guid? AcademicPlanRecordParentId { get; set; }

		/// <summary>
		/// Является родительской для дисциплин по выбору
		/// </summary>
        public bool IsParent { get; set; }

		/// <summary>
		/// Является дисциплиной по выбору
		/// </summary>
        public bool IsChild { get; set; }

		/// <summary>
		/// Является факультативной дисциплиной
		/// </summary>
		public bool IsFacultative { get; set; }

		/// <summary>
		/// Участвует в расчетах нагрузки
		/// </summary>
		public bool IsUseInWorkload { get; set; }

		/// <summary>
		/// Является преподаваемой дисциплиной по учебному плану (есть группы, которые проходят эту дисциплину в этом учебном году)
		/// </summary>
		public bool IsActiveSemester { get; set; }
    }
}