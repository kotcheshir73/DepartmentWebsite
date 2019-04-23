﻿using Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.AcademicYearData
{
    /// <summary>
    /// Класс, хранящий информацию о нагрузке преподавателя по дисциплине
    /// </summary>
    [DataContract]
    public class AcademicPlanRecordMission : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid LecturerId { get; set; }

        [Required]
        [DataMember]
        public Guid AcademicPlanRecordElementId { get; set; }

        [Required]
        [DataMember]
        public decimal Hours { get; set; }

        //-------------------------------------------------------------------------

        public virtual AcademicPlanRecordElement AcademicPlanRecordElement { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        //-------------------------------------------------------------------------
    }
}