using DepartmentModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий информацию об аудиториях, указанных в расчасовке
    /// </summary>
    [DataContract]
    public class GraficClassroom : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid GraficId { get; set; }

        [Required]
        [DataMember]
        public Guid TimeNormId { get; set; }

        [DataMember]
        public string ClassroomDescription { get; set; }
        
        //-------------------------------------------------------------------------

        public virtual Grafic Grafic { get; set; }
        
        public virtual TimeNorm TimeNorm { get; set; }

        //-------------------------------------------------------------------------

    }
}
