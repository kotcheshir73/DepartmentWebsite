namespace DepartmentDAL.Models
{
	/// <summary>
	/// Информация по студенту в рамках дисциплины
	/// В какой подгруппе, какой у него вариант
	/// </summary>
	public class DisciplineStudentRecord : BaseEntity
	{
		public int Variant { get; set; }

		public int SubGroup { get; set; }

		public long DisciplineId { get; set; }

		public virtual Discipline Discipline { get; set; }

		public long StudentId { get; set; }

		public virtual Student Student { get; set; }
	}
}
