using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.AcademicYear
{
    /// <summary>
    /// Класс, хранящий информацию о записи расчасовки
    /// </summary>
    [DataContract]
    public class DisciplineTimeDistributionRecord : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DisciplineTimeDistributionId { get; set; }

        [Required]
        [DataMember]
        public Guid TimeNormId { get; set; }

        [Required]
        [DataMember]
        public int WeekNumber { get; set; }
        
        [DataMember]
        public double Hours { get; set; }
        
        //-------------------------------------------------------------------------

        public virtual DisciplineTimeDistribution DisciplineTimeDistribution { get; set; }
        
        public virtual TimeNorm TimeNorm { get; set; }

        //-------------------------------------------------------------------------

    }
}