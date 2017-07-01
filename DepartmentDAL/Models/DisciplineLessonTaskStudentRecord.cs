using DepartmentDAL.Enums;

namespace DepartmentDAL.Models
{
	public class DisciplineLessonTaskStudentRecord : BaseEntity
	{
		public DisciplineLessonTaskStudentResult Result { get; set; }

		public string Comment { get; set; }

		public long DisciplineLessonTaskId { get; set; }

		public virtual DisciplineLessonTask DisciplineLessonTask { get; set; }

		public long StudentId { get; set; }

		public virtual Student Student { get; set; }
	}
}
