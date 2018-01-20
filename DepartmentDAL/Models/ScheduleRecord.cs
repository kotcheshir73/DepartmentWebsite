using System.Runtime.Serialization;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий основу занятия (пара в семестре, зачет, экзамен или консультация)
    /// </summary>
    [DataContract]
    public class ScheduleRecord
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public long SeasonDatesId { get; set; }

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

        [DataMember]
        public string ClassroomId { get; set; }

        [DataMember]
        public long? StudentGroupId { get; set; }

        [DataMember]
        public long? LecturerId { get; set; }

        [DataMember]
        public long? DisciplineId { get; set; }

        public virtual SeasonDates SeasonDates { get; set; }

        public virtual Classroom Classroom { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        public virtual Lecturer Lecturer { get; set; }

		public virtual Discipline Discipline { get; set; }
	}
}
