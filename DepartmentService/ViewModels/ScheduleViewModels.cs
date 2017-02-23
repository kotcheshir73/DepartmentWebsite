using System;

namespace DepartmentService.ViewModels
{
    public class ScheduleStopWordViewModel
    {
        public long Id { get; set; }

        public string StopWord { get; set; }

        public string StopWordType { get; set; }
    }

    public class SemesterRecordShortViewModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public string LessonType { get; set; }

        public bool IsStreaming { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }
    }

    public class SemesterRecordViewModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public string LessonType { get; set; }

        public bool IsStreaming { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string ClassroomId { get; set; }

        public string Classroom { get; set; }

        public long? LecturerId { get; set; }

        public string Lecturer { get; set; }

        public string Discipline { get; set; }

        public long? StudentGroupId { get; set; }

        public string StudentGroup { get; set; }
    }

    public class ConsultationRecordShortViewModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public DateTime DateConsultation { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }
    }

    public class ConsultationRecordViewModel
    {
        public long Id { get; set; }

        public DateTime DateConsultation { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string ClassroomId { get; set; }

        public string Classroom { get; set; }

        public long? LecturerId { get; set; }

        public string Lecturer { get; set; }

        public string Discipline { get; set; }

        public long? StudentGroupId { get; set; }

        public string StudentGroup { get; set; }
    }

    public class OffsetRecordShortViewModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public bool IsStreaming { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }
    }

    public class OffsetRecordViewModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public bool IsStreaming { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string ClassroomId { get; set; }

        public string Classroom { get; set; }

        public long? LecturerId { get; set; }

        public string Lecturer { get; set; }

        public string Discipline { get; set; }

        public long? StudentGroupId { get; set; }

        public string StudentGroup { get; set; }
    }

    public class ExaminationRecordShortViewModel
    {
        public long Id { get; set; }

        public DateTime DateConsultation { get; set; }

        public DateTime DateExamination { get; set; }

        public string LessonType { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }
    }

    public class ExaminationRecordViewModel
    {
        public long Id { get; set; }

        public DateTime DateConsultation { get; set; }

        public DateTime DateExamination { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string ClassroomId { get; set; }

        public string Classroom { get; set; }

        public long? LecturerId { get; set; }

        public string Lecturer { get; set; }

        public string Discipline { get; set; }

        public long? StudentGroupId { get; set; }

        public string StudentGroup { get; set; }
    }
}
