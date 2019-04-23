﻿using Enums;
using Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.AcademicYearData
{
    /// <summary>
    /// Класс, хранящий инорфмацио по контингенту
    /// </summary>
    [DataContract]
    public class Contingent : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid AcademicYearId { get; set; }

        [Required]
        [DataMember]
        public Guid EducationDirectionId { get; set; }

        [Required]
        [DataMember]
        public string ContingentName { get; set; }

        [Required]
        [DataMember]
        public AcademicCourse Course { get; set; }

        [Required]
        [DataMember]
        public int CountGroups { get; set; }

        [Required]
        [DataMember]
        public int CountStudetns { get; set; }

        [Required]
        [DataMember]
        public int CountSubgroups { get; set; }

        //-------------------------------------------------------------------------

        public virtual AcademicYear AcademicYear {	get; set; }

		public virtual EducationDirection EducationDirection { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("ContingentId")]
		public virtual List<AcademicPlanRecord> AcademicPlanRecords { get; set; }
	}
}