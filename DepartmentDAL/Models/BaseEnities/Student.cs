using DepartmentDAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    [DataContract]
    public class Student : BaseEntity
    {
        [MaxLength(10)]
        [Required]
        [DataMember]
        public string NumberOfBook { get; set; }

        [DataMember]
        public Guid? StudentGroupId { get; set; }
        
        [MaxLength(20)]
        [Required]
        [DataMember]
        public string FirstName { get; set; }
        
        [MaxLength(30)]
        [Required]
        [DataMember]
        public string LastName { get; set; }
        
        [MaxLength(30)]
        [DataMember]
        public string Patronymic { get; set; }
        
		[MaxLength(150)]
		[Required]
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public StudentState StudentState { get; set; }
        
        [DataMember]
        public string Description { get; set; }
        
        [DataMember]
        public byte[] Photo { get; set; }

        [Required]
        [DataMember]
        public bool IsSteward { get; set; }

        //-------------------------------------------------------------------------

        public virtual StudentGroup StudentGroup { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("StudentId")]
        public virtual List<StudentHistory> StudentHistory { get; set; }

		[ForeignKey("StudentId")]
		public virtual List<DisciplineStudentRecord> DisciplineStudentRecords { get; set; }

		[ForeignKey("StudentId")]
		public virtual List<DisciplineLessonStudentRecord> DisciplineLessonStudentRecords { get; set; }

		[ForeignKey("StudentId")]
		public virtual List<DisciplineLessonTaskStudentRecord> DisciplineLessonTaskStudentRecords { get; set; }
	}
}
