using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.AcademicYear
{
    /// <summary>
    /// Класс, хранящий информацию об аудиториях, указанных в расчасовке
    /// </summary>
    [DataContract]
    public class DisciplineTimeDistributionClassroom : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DisciplineTimeDistributionId { get; set; }

        [Required]
        [DataMember]
        public Guid TimeNormId { get; set; }

        [DataMember]
        public string ClassroomDescription { get; set; }
        
        //-------------------------------------------------------------------------

        public virtual DisciplineTimeDistribution DisciplineTimeDistribution { get; set; }
        
        public virtual TimeNorm TimeNorm { get; set; }

        //-------------------------------------------------------------------------
    }
}