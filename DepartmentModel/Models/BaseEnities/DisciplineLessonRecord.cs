using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentModel.Models.BaseEnities
{
    /// <summary>
    /// Класс, описывающий проведение занятия
    /// </summary>
    [DataContract]
    public class DisciplineLessonRecord : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid DisciplineLessonId { get; set; }

        [Required]
        [DataMember]
        public DateTime Date { get; set; }
        
        [DataMember]
        public string Subgroup { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineLesson DisciplineLesson { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("DisciplineLessonRecordId")]
        public virtual List<DisciplineLessonStudentRecord> DisciplineLessonStudentRecords { get; set; }
    }
}
