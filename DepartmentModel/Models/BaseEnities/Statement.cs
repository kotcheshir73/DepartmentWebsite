using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию о ведомости по предмету у преподавателя
    /// </summary>
    [DataContract]
    public class Statement : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid LecturerId { get; set; }

        [Required]
        [DataMember]
        public Guid AcademicPlanRecordId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentGroupId { get; set; }

        [Required]
        [DataMember]
        public AcademicCourse Course { get; set; }

        [Required]
        [DataMember]
        public TypeOfTest TypeOfTest { get; set; }

        [DataMember]
        public Semesters? Semester { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        //-------------------------------------------------------------------------

        public virtual Lecturer Lecturer { get; set; }
        
        public virtual AcademicPlanRecord AcademicPlanRecord { get; set; }
        
        public virtual StudentGroup StudentGroup { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("StatementId")]
        public virtual List<StatementRecord> StatementRecords { get; set; }
    }
}
