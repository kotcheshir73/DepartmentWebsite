﻿using Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.AcademicYearData
{
    /// <summary>
    /// Преподавательская ставка
    /// </summary>
    [DataContract]
    public class LecturerWorkload : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid LecturerId { get; set; }

        [Required]
        [DataMember]
        public Guid AcademicYearId { get; set; }

        [DataMember]
        public double Workload { get; set; }

        //-------------------------------------------------------------------------

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        //-------------------------------------------------------------------------

    }
}