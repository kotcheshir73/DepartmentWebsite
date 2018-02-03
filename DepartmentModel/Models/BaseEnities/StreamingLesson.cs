using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, хранящий в себе сведенья о потоковых занятиях
    /// </summary>
    [DataContract]
    public class StreamingLesson : BaseEntity
    {
        [Required]
        [DataMember]
        public string IncomingGroups { get; set; }

        [Required]
        [DataMember]
        public string StreamName { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
    }
}
