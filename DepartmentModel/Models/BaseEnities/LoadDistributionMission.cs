using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Поручение преподавателю на основе распределения нагрузки
    /// </summary>
    [DataContract]
    public class LoadDistributionMission : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid LoadDistributionRecordId { get; set; }

        [Required]
        [DataMember]
        public Guid LecturerId { get; set; }

        [Required]
        [DataMember]
        public decimal Hours { get; set; }

        //-------------------------------------------------------------------------

        public virtual Lecturer Lecturer { get; set; }

		public virtual LoadDistributionRecord LoadDistributionRecord { get;set; }

        //-------------------------------------------------------------------------
    }
}
