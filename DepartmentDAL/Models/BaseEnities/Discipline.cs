using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий дисциплину
    /// </summary>
    [DataContract]
    public class Discipline : BaseEntity
	{
        [Required]
        [DataMember]
        public Guid DisciplineBlockId { get; set; }

		[MaxLength(200)]
		[Required]
        [DataMember]
        public string DisciplineName { get; set; }
        
        [MaxLength(20)]
        [DataMember]
        public string DisciplineShortName { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineBlock DisciplineBlock { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("DisciplineId")]
		public virtual List<AcademicPlanRecord> AcademicPlanRecord { get; set; }

		[ForeignKey("DisciplineId")]
		public virtual List<DisciplineLesson> DisciplineLessons { get; set; }

		[ForeignKey("DisciplineId")]
		public virtual List<DisciplineStudentRecord> DisciplineStudentRecords { get; set; }
	}
}
