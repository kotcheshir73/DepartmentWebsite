using DepartmentModel.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ScheduleModels.Models
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
        public Guid SeasonDatesId { get; set; }

        [DataMember]
        public Guid? ClassroomId { get; set; }

        [DataMember]
        public Guid? StudentGroupId { get; set; }

        [DataMember]
        public Guid? LecturerId { get; set; }

        [DataMember]
        public Guid? DisciplineId { get; set; }

        [DataMember]
        public string NotParseRecord { get; set; }

        [DataMember]
        public string LessonDiscipline { get; set; }

        [DataMember]
        public string LessonLecturer { get; set; }

        [DataMember]
        public string LessonGroup { get; set; }

        [DataMember]
        public string LessonClassroom { get; set; }

        //-------------------------------------------------------------------------

        public virtual SeasonDates SeasonDates { get; set; }

        public virtual Classroom Classroom { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        public virtual Lecturer Lecturer { get; set; }

		public virtual Discipline Discipline { get; set; }

        //-------------------------------------------------------------------------

        public ScheduleRecord()
        {
            Id = Guid.NewGuid();
        }
    }
}