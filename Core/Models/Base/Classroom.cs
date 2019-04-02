using Models.Enums;
using Models.LaboratoryHead;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Base
{
    /// <summary>
    /// Класс, описывающий аудиторию
    /// </summary>
    [DataContract]
    public class Classroom : BaseEntity
    {
        [DataMember]
        public string Number { get; set; }

        [Required]
        [DataMember]
        public ClassroomTypes ClassroomType { get; set; }

        [Required]
        [DataMember]
        public int Capacity { get; set; }

        [Required]
        [DataMember]
        public bool NotUseInSchedule { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("ClassroomId")]
        public virtual List<MaterialTechnicalValue> MaterialTechnicalValues { get; set; }
    }
}