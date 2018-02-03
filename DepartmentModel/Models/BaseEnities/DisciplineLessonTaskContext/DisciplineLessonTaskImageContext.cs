using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий контент к заданию (картинка)
    /// </summary>
    [DataContract]
    public class DisciplineLessonTaskImageContext : DisciplineLessonTaskContext
    {
        [Required]
        [DataMember]
        public byte[] Image { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
    }
}
