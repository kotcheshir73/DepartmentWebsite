using Enums;
using Models.Attributes;
using Models.Examination;
using Models.LearningProgress;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Models.Base
{
    [DataContract]
    [ClassUse("StudentGroup", "StudentGroupId", "Студент привязан к группе, если он не отчисленный")]
    public class Student : BaseEntity
    {
        [MaxLength(10)]
        [Required]
        [DataMember]
        public string NumberOfBook { get; set; }

        [DataMember]
        public Guid? StudentGroupId { get; set; }
        
        [MaxLength(150)]
        [Required]
        [DataMember]
        public string FirstName { get; set; }
        
        [MaxLength(150)]
        [Required]
        [DataMember]
        public string LastName { get; set; }
        
        [MaxLength(150)]
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
        public virtual List<StudentOrderBlockStudent> StudentOrderBlockStudents { get; set; }

        [ForeignKey("StudentId")]
        public virtual List<StatementRecord> StatementRecords { get; set; }

        [ForeignKey("StudentId")]
        public virtual List<ExaminationList> ExaminationLists { get; set; }

        [ForeignKey("StudentId")]
		public virtual List<DisciplineStudentRecord> DisciplineStudentRecords { get; set; }

        [ForeignKey("StudentId")]
        public virtual List<DisciplineLessonConductedStudent> DisciplineLessonConductedStudents { get; set; }

        [ForeignKey("StudentId")]
		public virtual List<DisciplineLessonTaskStudentAccept> DisciplineLessonTaskStudentRecords { get; set; }

        //-------------------------------------------------------------------------

        public override string ToString()
        {
            StringBuilder result = new StringBuilder(LastName);
            if (!string.IsNullOrEmpty(FirstName))
            {
                result.Append(" ");
                result.Append(FirstName[0]);
                result.Append(".");
            }
            if (!string.IsNullOrEmpty(Patronymic))
            {
                result.Append(" ");
                result.Append(Patronymic[0]);
                result.Append(".");
            }
            return result.ToString();
        }
    }
}