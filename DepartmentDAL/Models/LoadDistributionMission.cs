using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Поручение преподавателю на основе распределения нагрузки
    /// </summary>
    [DataContract]
    public class LoadDistributionMission : BaseEntity
    {
        [DataMember]
        public long LoadDistributionRecordId { get; set; }

        [DataMember]
        public long LecturerId { get; set; }

        [DataMember]
        public decimal Hours { get; set; }

		public virtual Lecturer Lecturer { get; set; }

		public virtual LoadDistributionRecord LoadDistributionRecord { get;set;}
	}
}
