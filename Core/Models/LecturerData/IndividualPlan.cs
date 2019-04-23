using Models.AcademicYearData;
using Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.LecturerData
{
    /// <summary>
    /// Класс, описывающий годовой индивидуальный план
    /// </summary>
    [DataContract]
    public class IndividualPlan : BaseEntity
    {
        [DataMember]
        public Guid LecturerId { get; set; }

        [DataMember]
        public Guid AcademicYearId { get; set; }


        //-------------------------------------------------------------------------

        public virtual Lecturer Lecturer { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("IndividualPlanId")]
        public virtual List<IndividualPlanRecord> IndividualPlanRecords { get; set; }
    }
}