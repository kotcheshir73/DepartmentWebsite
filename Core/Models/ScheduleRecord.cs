using Enums;
using Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models
{
    /// <summary>
    /// Класс, описывающий основу занятия (пара в семестре, зачет, экзамен или консультация)
    /// </summary>
    [DataContract]
    public class ScheduleRecord
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid? ClassroomId { get; set; }

        [DataMember]
        public Guid? DisciplineId { get; set; }

        [DataMember]
        public Guid? LecturerId { get; set; }

        [DataMember]
        public Guid? StudentGroupId { get; set; }

        [Required]
        [DataMember]
        public LessonTypes LessonType { get; set; }

        [Required]
        [DataMember]
        public DateTime ScheduleDate { get; set; }

        [DataMember]
        public string NotParseRecord { get; set; }

        [Required]
        [DataMember]
        public string LessonClassroom { get; set; }

        [Required]
        [DataMember]
        public string LessonDiscipline { get; set; }

        [Required]
        [DataMember]
        public string LessonLecturer { get; set; }

        [Required]
        [DataMember]
        public string LessonStudentGroup { get; set; }

        /// <summary>
        /// При загрузке расписания отметка, проверена пара или нет
        /// </summary>
        [NotMapped]
        public bool Checked { get; set; }

        //-------------------------------------------------------------------------

        public virtual Classroom Classroom { get; set; }

		public virtual Discipline Discipline { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        //-------------------------------------------------------------------------

        public ScheduleRecord()
        {
            Id = Guid.NewGuid();
            Checked = false;
        }
    }
}