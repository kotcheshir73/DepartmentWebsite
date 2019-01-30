using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию о записи расчасовки
    /// </summary>
    [DataContract]
    public class GraficRecord : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid GraficId { get; set; }

        [Required]
        [DataMember]
        public Guid TimeNormId { get; set; }

        [Required]
        [DataMember]
        public int WeekNumber { get; set; }
        
        [DataMember]
        public double Hours { get; set; }
        
        //-------------------------------------------------------------------------

        public virtual Grafic Grafic { get; set; }
        
        public virtual TimeNorm TimeNorm { get; set; }

        //-------------------------------------------------------------------------

    }
}
