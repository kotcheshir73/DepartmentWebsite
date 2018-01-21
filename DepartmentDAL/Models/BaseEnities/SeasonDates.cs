﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий даты для учебного полугодья
    /// </summary>
    [DataContract]
    public class SeasonDates : BaseEntity
    {
        [Required]
        [DataMember]
        public string Title { get; set; }

        [Required]
        [DataMember]
        public DateTime DateBeginSemester { get; set; }

        [Required]
        [DataMember]
        public DateTime DateEndSemester { get; set; }

        [Required]
        [DataMember]
        public DateTime DateBeginOffset { get; set; }

        [Required]
        [DataMember]
        public DateTime DateEndOffset { get; set; }

        [Required]
        [DataMember]
        public DateTime DateBeginExamination { get; set; }

        [Required]
        [DataMember]
        public DateTime DateEndExamination { get; set; }

        [DataMember]
        public DateTime? DateBeginPractice { get; set; }

        [DataMember]
        public DateTime? DateEndPractice { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
    }
}