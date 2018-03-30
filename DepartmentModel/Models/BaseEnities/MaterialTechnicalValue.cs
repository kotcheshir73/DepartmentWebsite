using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, описывающий материально-технические ценности кафедры
    /// </summary>
    [DataContract]
    public class MaterialTechnicalValue : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid ClassroomId { get; set; }

        [MaxLength(50)]
        [Required]
        [DataMember]
        public string InventoryNumber { get; set; }

        [MaxLength(200)]
        [Required]
        [DataMember]
        public string FullName { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public decimal Cost { get; set; }

        public string DeleteReason { get; set; }

        //-------------------------------------------------------------------------

        public virtual Classroom Classroom { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("MaterialTechnicalValueId")]
        public virtual List<SoftwareRecord> SoftwareRecords { get; set; }
    }
}
