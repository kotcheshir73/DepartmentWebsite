using System;

namespace DepartmentService.ViewModels
{
    public class ScheduleLessonTimeViewModel
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public string Title { get; set; }

        public string TimeBeginLesson { get; set; }

        public string TimeEndLesson { get; set; }

        public DateTime DateBeginLesson { get; set; }

        public DateTime DateEndLesson { get; set; }
    }

    public class ScheduleStopWordViewModel
    {
        public long Id { get; set; }

        public string StopWord { get; set; }

		public string StopWordReplace { get; set; }

		public string StopWordType { get; set; }
    }

    public class SemesterRecordShortViewModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

		public string NotParseRecord { get; set; }

		public string LessonType { get; set; }

        public bool IsStreaming { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string Text
        {
            get
            {
                return string.Format("{0} {1}{2}{3}{2}{4}", LessonType, LessonDiscipline, Environment.NewLine, LessonLecturer, LessonGroup);
            }
        }
    }

    public class SemesterRecordViewModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

		public string NotParseRecord { get; set; }

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

    public class OffsetRecordShortViewModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

		public string NotParseRecord { get; set; }

		public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string Text
        {
            get
            {
                return string.Format("{0}{1}{2}{1}{3}", LessonDiscipline, Environment.NewLine, LessonLecturer, LessonGroup);
            }
        }
    }

    public class OffsetRecordViewModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

		public string NotParseRecord { get; set; }

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

		public string NotParseRecord { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string Text
        {
            get
            {
                return string.Format("{0}{1}{2}{1}{3}", LessonDiscipline, Environment.NewLine, LessonLecturer, LessonGroup);
            }
        }
    }

    public class ExaminationRecordViewModel
    {
        public long Id { get; set; }

		public DateTime DateConsultation { get; set; }

        public DateTime DateExamination { get; set; }

		public string NotParseRecord { get; set; }

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

		public string NotParseRecord { get; set; }

		public DateTime DateConsultation { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string Text
        {
            get
            {
                return string.Format("{0} конс.{1}{2}{1}{3}", LessonDiscipline, Environment.NewLine, LessonLecturer, LessonGroup);
            }
        }
    }

    public class ConsultationRecordViewModel
    {
        public long Id { get; set; }

        public DateTime DateConsultation { get; set; }

		public string NotParseRecord { get; set; }

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
