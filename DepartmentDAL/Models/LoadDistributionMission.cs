namespace DepartmentDAL.Models
{
	/// <summary>
	/// Поручение преподавателю на основе распределения нагрузки
	/// </summary>
	public class LoadDistributionMission : BaseEntity
	{
		public long LoadDistributionRecordId { get; set; }

		public long LecturerId { get; set; }

		public decimal Hours { get; set; }

		public virtual Lecturer Lecturer { get; set; }

		public virtual LoadDistributionRecord LoadDistributionRecord { get;set;}
	}
}
