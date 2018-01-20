using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Информация по студенту в рамках дисциплины
    /// В какой подгруппе, какой у него вариант
    /// </summary>
    [DataContract]
    public class DisciplineStudentRecord : BaseEntity
    {
        [DataMember]
        public int Variant { get; set; }

        [DataMember]
        public int SubGroup { get; set; }

        [DataMember]
        public long DisciplineId { get; set; }

        [DataMember]
        public string StudentId { get; set; }

		public virtual Discipline Discipline { get; set; }

		public virtual Student Student { get; set; }
	}
}
