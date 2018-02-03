using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий контент к заданию
    /// На данный момент 2 варианта: текст или картинка
    /// </summary>
    [DataContract]
    public class DisciplineLessonTaskContext : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DisciplineLessonTaskId { get; set; }

        [Required]
        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineLessonTask DisciplineLessonTask { get; set; }

        //-------------------------------------------------------------------------
    }
}
