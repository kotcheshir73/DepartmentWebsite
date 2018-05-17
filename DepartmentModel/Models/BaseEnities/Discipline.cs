using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
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

        [DataMember]
        public Guid? DisciplineParentId { get; set; }

        [DataMember]
        public bool IsParent { get; set; }

        [MaxLength(200)]
		[Required]
        [DataMember]
        public string DisciplineName { get; set; }
        
        [MaxLength(20)]
        [DataMember]
        public string DisciplineShortName { get; set; }

        [MaxLength(200)]
        [DataMember]
        public string DisciplineBlueAsteriskName { get; set; }

        [NotMapped]
        public string DisciplineBlueAsteriskCode { get; set; }

        [NotMapped]
        public string DisciplineBlueAsteriskPracticCode { get; set; }

        [NotMapped]
        public bool NotSelected { get; set; }

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
