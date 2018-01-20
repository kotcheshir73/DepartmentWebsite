using DepartmentDAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хранящий инорфмацио по контингенту
    /// </summary>
    [DataContract]
    public class Contingent : BaseEntity
    {
        [DataMember]
        public long AcademicYearId { get; set; }

        [DataMember]
        public long EducationDirectionId { get; set; }

        [DataMember]
        public AcademicCourse Course { get; set; }

        [DataMember]
        public int CountStudetns { get; set; }

        [DataMember]
        public int CountSubgroups { get; set; }

		public virtual AcademicYear AcademicYear {	get; set; }

		public virtual EducationDirection EducationDirection { get; set; }

		[ForeignKey("ContingentId")]
		public virtual List<LoadDistributionRecord> LoadDistributionRecord { get; set; }
	}
}
