﻿using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию по нормам времени
    /// </summary>
    [DataContract]
    public class TimeNorm : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid KindOfLoadId { get; set; }

        [Required]
        [DataMember]
        public Guid AcademicYearId { get; set; }

        [MaxLength(50)]
		[Required]
        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public decimal? Hours { get; set; }
        
        [DataMember]
        public decimal? NumKoef { get; set; }

        [Required]
        [DataMember]
        public KindOfLoadType KindOfLoadType { get; set; }

        [Required]
        [DataMember]
        public TimeNormKoef TimeNormKoef { get; set; }

        //-------------------------------------------------------------------------

        public virtual KindOfLoad KindOfLoad { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("TimeNormId")]
		public virtual List<LoadDistributionRecord> LoadDistributionRecord { get; set; }
	}
}
