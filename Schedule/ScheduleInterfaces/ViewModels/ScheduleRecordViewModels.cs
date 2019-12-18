using Enums;
using System;
using System.ComponentModel;

namespace ScheduleInterfaces.ViewModels
{
    public class ScheduleRecordShortViewModel
    {
        public Guid Id { get; set; }

        public string NotParseRecord { get; set; }

        [DisplayName("Дисциплина")]
        public string LessonDiscipline { get; set; }

        [DisplayName("Преподаватель")]
        public string LessonLecturer { get; set; }

        [DisplayName("Группа")]
        public string LessonGroup { get; set; }

        [DisplayName("Аудитория")]
        public string LessonClassroom { get; set; }

        public DateTime ScheduleDate { get; set; }

        public int TimeSpanMinutes { get; set; }

        public LessonTypes LessonType { get; set; }

        public ScheduleRecordType ScheduleRecordType { get; set; }
    }

    public class ScheduleRecordViewModel
    {
        public Guid Id { get; set; }

        public string NotParseRecord { get; set; }

        [DisplayName("Аудитория")]
        public string LessonClassroom { get; set; }

        [DisplayName("Дисциплина")]
        public string LessonDiscipline { get; set; }

        [DisplayName("Преподаватель")]
        public string LessonLecturer { get; set; }

        [DisplayName("Группа")]
        public string LessonStudentGroup { get; set; }

        public Guid? ClassroomId { get; set; }

        [DisplayName("Аудитория")]
        public string Classroom { get; set; }

        public Guid? DisciplineId { get; set; }

        [DisplayName("Дисциплина")]
        public string Discipline { get; set; }

        public Guid? LecturerId { get; set; }

        [DisplayName("Преподаватель")]
        public string Lecturer { get; set; }

        public Guid? StudentGroupId { get; set; }

        [DisplayName("Группа")]
        public string StudentGroup { get; set; }

        public DateTime ScheduleDate { get; set; }

        public int TimeSpanMinutes { get; set; }

        public LessonTypes LessonType { get; set; }

        public ScheduleRecordType ScheduleRecordType { get; set; }
    }
}