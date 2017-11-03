using DepartmentDAL.Enums;

namespace DepartmentDAL.Models
{
	public class DisciplineLessonStudentRecord : BaseEntity
	{
		public DisciplineLessonStudentStatus Status { get; set; }

		public long DisciplineLessonId { get; set; }

		public virtual DisciplineLesson DisciplineLesson { get; set; }

		public string StudentId { get; set; }

		public virtual Student Student { get; set; }
	}
}
