using Models.AcademicYearData;
using Models.Base;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Examination
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
        public Guid DisciplineId { get; set; }

        [Required]
        [DataMember]
        public Guid AcademicYearId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentGroupId { get; set; }

        [Required]
        [DataMember]
        public TypeOfTest TypeOfTest { get; set; }

        [DataMember]
        public bool IsMain { get; set; }

        //-------------------------------------------------------------------------

        public virtual Lecturer Lecturer { get; set; }
        
        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Discipline Discipline { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("StatementId")]
        public virtual List<StatementRecord> StatementRecords { get; set; }
    }
}