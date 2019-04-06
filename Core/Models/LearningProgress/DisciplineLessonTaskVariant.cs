using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.LearningProgress
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