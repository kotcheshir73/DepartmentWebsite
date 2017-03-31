using System;

namespace DepartmentDAL.Enums
{
	[Flags]
	public enum AcademicCourse : int
	{
		Course_1 = 1,

		Course_2 = 2,

		Course_3 = 4,

		Course_4 = 8,

		Course_5 = 16,

		Course_6 = 32
	}
}
