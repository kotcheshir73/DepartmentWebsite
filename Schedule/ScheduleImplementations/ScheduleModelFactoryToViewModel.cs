using Enums;
using Models;
using Models.Schedule;
using ScheduleImplementations.Helpers;
using ScheduleInterfaces.ViewModels;

namespace ScheduleImplementations
{
    public static class ScheduleModelFactoryToViewModel
    {
        public static ScheduleRecordViewModel CreateScheduleRecordViewModel(this ScheduleRecord entity, ScheduleRecordViewModel record = null)
        {
            if(record == null)
            {
                record = new ScheduleRecordViewModel();
            }

            record.Id = entity.Id;
            record.ClassroomId = entity.ClassroomId;
            record.Classroom = entity.Classroom?.ToString();
            record.DisciplineId = entity.DisciplineId;
            record.Discipline = entity.Discipline?.ToString();
            record.LecturerId = entity.LecturerId;
            record.Lecturer = entity.Lecturer?.ToString();
            record.StudentGroupId = entity.StudentGroupId;
            record.StudentGroup = entity.StudentGroup?.ToString();
            record.ScheduleDate = entity.ScheduleDate;
            record.NotParseRecord = entity.NotParseRecord;
            record.LessonClassroom = entity.LessonClassroom;
            record.LessonStudentGroup = entity.LessonStudentGroup;
            record.LessonDiscipline = entity.LessonDiscipline;
            record.LessonLecturer = entity.LessonLecturer;

            return record;
        }

        public static SemesterRecordViewModel CreateRecordViewModel(this SemesterRecord entity)
        {
            var record = entity.CreateScheduleRecordViewModel(new SemesterRecordViewModel()) as SemesterRecordViewModel;

            record.Week = ScheduleHelper.GetWeek(entity.ScheduleDate);
            record.Day = ScheduleHelper.GetDay(entity.ScheduleDate);
            record.Lesson = ScheduleHelper.GetLesson(entity.ScheduleDate);
            record.LessonType = entity.LessonType;
            record.ScheduleRecordType = ScheduleRecordType.Semester;

            return record;
        }

        public static OffsetRecordViewModel CreateRecordViewModel(this OffsetRecord entity)
        {
            var record = entity.CreateScheduleRecordViewModel(new OffsetRecordViewModel()) as OffsetRecordViewModel;

            record.Lesson = ScheduleHelper.GetLesson(entity.ScheduleDate);
            record.LessonType = LessonTypes.зачет;
            record.ScheduleRecordType = ScheduleRecordType.Offset;

            return record;
        }

        public static ExaminationRecordViewModel CreateRecordViewModel(this ExaminationRecord entity)
        {
            var record = entity.CreateScheduleRecordViewModel(new ExaminationRecordViewModel()) as ExaminationRecordViewModel;

            record.DateConsultation = entity.DateConsultation;
            record.ConsultationClassroomId = entity.ConsultationClassroomId;
            record.ConsultationClassroom = entity.ConsultationClassroom?.ToString();
            record.LessonConsultationClassroom = entity.LessonConsultationClassroom;
            record.LessonType = LessonTypes.экзамен;
            record.ScheduleRecordType = ScheduleRecordType.Examination;

            return record;
        }

        public static ConsultationRecordViewModel CreateRecordViewModel(this ConsultationRecord entity)
        {
            var record = entity.CreateScheduleRecordViewModel(new ConsultationRecordViewModel()) as ConsultationRecordViewModel;

            record.ConsultationTime = entity.ConsultationTime;
            record.LessonType = LessonTypes.конс;
            record.ScheduleRecordType = ScheduleRecordType.Consultation;

            return record;
        }

        public static ScheduleRecordShortViewModel CreateScheduleRecordShortViewModel(this ScheduleRecord entity, ScheduleRecordShortViewModel record = null)
        {
            if (record == null)
            {
                record = new ScheduleRecordShortViewModel();
            }

            record.Id = entity.Id;
            record.ScheduleDate = entity.ScheduleDate;
            record.NotParseRecord = entity.NotParseRecord;
            record.LessonLecturer = ScheduleHelper.GetLessonLecturer(entity);
            record.LessonDiscipline = ScheduleHelper.GetLessonDiscipline(entity);
            record.LessonGroup = ScheduleHelper.GetLessonGroup(entity);
            record.LessonClassroom = ScheduleHelper.GetLessonClassroom(entity);

            return record;
        }

        public static SemesterRecordShortViewModel CreateRecordShortViewModel(this SemesterRecord entity)
        {
            var record = entity.CreateScheduleRecordShortViewModel(new SemesterRecordShortViewModel()) as SemesterRecordShortViewModel;

            record.Week = ScheduleHelper.GetWeek(entity.ScheduleDate);
            record.Day = ScheduleHelper.GetDay(entity.ScheduleDate);
            record.Lesson = ScheduleHelper.GetLesson(entity.ScheduleDate);
            record.LessonType = entity.LessonType;
            record.ScheduleRecordType = ScheduleRecordType.Semester;

            return record;
        }

        public static OffsetRecordShortViewModel CreateRecordShortViewModel(this OffsetRecord entity)
        {
            var record = entity.CreateScheduleRecordShortViewModel(new OffsetRecordShortViewModel()) as OffsetRecordShortViewModel;

            record.Lesson = ScheduleHelper.GetLesson(entity.ScheduleDate);
            record.LessonType = LessonTypes.зачет;
            record.ScheduleRecordType = ScheduleRecordType.Offset;

            return record;
        }

        public static ExaminationRecordShortViewModel CreateRecordShortViewModel(this ExaminationRecord entity)
        {
            var record = entity.CreateScheduleRecordShortViewModel(new ExaminationRecordShortViewModel()) as ExaminationRecordShortViewModel;

            record.DateConsultation = entity.DateConsultation;
            record.LessonConsultationClassroom = entity.LessonConsultationClassroom;
            record.LessonType = LessonTypes.экзамен;
            record.ScheduleRecordType = ScheduleRecordType.Examination;

            return record;
        }

        public static ConsultationRecordShortViewModel CreateRecordShortViewModel(this ConsultationRecord entity)
        {
            var record = entity.CreateScheduleRecordShortViewModel(new ConsultationRecordShortViewModel()) as ConsultationRecordShortViewModel;

            record.ConsultationTime = entity.ConsultationTime;
            record.LessonType = LessonTypes.конс;
            record.ScheduleRecordType = ScheduleRecordType.Consultation;

            return record;
        }
    }
}