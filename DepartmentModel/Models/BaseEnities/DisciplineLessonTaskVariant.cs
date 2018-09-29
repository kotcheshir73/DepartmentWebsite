using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, описывающий задачи по вариантам к занятиям дисциплины
    /// </summary>
    /// 
    [DataContract]
    public class DisciplineLessonTaskVariant : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DisciplineLessonTaskId { get; set; }

        [Required]
        [DataMember]
        public string VariantNumber { get; set; }

        [Required]
        [DataMember]
        public string VariantTask { get; set; }

        [Required]
        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineLessonTask DisciplineLessonTask { get; set; }
        
    }
}
