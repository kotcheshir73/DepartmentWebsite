using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию о расчасовке
    /// </summary>
    [DataContract]
    public class Grafic : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid AcademicPlanRecordId { get; set; }

        [Required]
        [DataMember]
        public Guid StudentGroupId { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public string CommentWishesOfTeacher { get; set; }

        //-------------------------------------------------------------------------
               
        public virtual AcademicPlanRecord AcademicPlanRecord { get; set; }
        
        public virtual StudentGroup StudentGroup { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("GraficId")]
        public virtual List<GraficRecord> GraficRecords { get; set; }

        [ForeignKey("GraficId")]
        public virtual List<GraficClassroom> GraficClassrooms { get; set; }
    }
}
