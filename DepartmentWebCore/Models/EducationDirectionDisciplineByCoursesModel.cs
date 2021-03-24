using System;

namespace DepartmentWebCore.Models
{
	public class EducationDirectionDisciplineByCoursesModel
	{
		public Guid DisciplineId { get; set; }

		public string DisciplineName { get; set; }

		public string TimeNormName { get; set; }

		public int Semester { get; set; }

		public bool InDepartment { get; set; }
	}
}