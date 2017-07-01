namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, хранящий контент к заданию
	/// На данный момент 2 варианта: текст или картинка
	/// </summary>
	public class DisciplineLessonTaskContext : BaseEntity
	{
		public int Order { get; set; }

		public long DisciplineLessonTaskId { get; set; }

		public virtual DisciplineLessonTask DisciplineLessonTask { get; set; }
	}
}
