using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий контент к заданию (текст)
    /// </summary>
    [DataContract]
    public class DisciplineLessonTaskTextContext : DisciplineLessonTaskContext
    {
        [Required]
        [DataMember]
        public string Text { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
    }
}
