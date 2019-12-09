using Models;
using Models.Schedule;
using ScheduleImplementations.Helpers;
using ScheduleInterfaces.BindingModels;
using ScheduleInterfaces.ViewModels;
using System;

namespace ScheduleImplementations
{
    public static class ScheduleModelFactoryToViewModel
    {
        public static SemesterRecordViewModel CreateRecordViewModel(this SemesterRecord entity)
        {
            return new SemesterRecordViewModel
            {
                Id = entity.Id,
                Day = entity.Day,
                Week = entity.Week,
                Lesson = entity.Lesson,
                NotParseRecord = entity.NotParseRecord,
                IsFirstHalfSemester = entity.IsFirstHalfSemester,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                LessonType = entity.LessonType,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom?.ToString(),
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline?.ToString(),
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer?.ToString(),
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup?.ToString()
            };
        }

        public static SemesterRecordShortViewModel CreateRecordShortViewModel(this SemesterRecord entity)
        {
            return new SemesterRecordShortViewModel
            {
                Id = entity.Id,
                Week = entity.Week,
                Day = entity.Day,
                Lesson = entity.Lesson,
                LessonType = entity.LessonType,
                LessonLecturer = ScheduleHelper.GetLessonLecturer(entity),
                LessonDiscipline = ScheduleHelper.GetLessonDiscipline(entity),
                LessonGroup = ScheduleHelper.GetLessonGroup(entity),
                LessonClassroom = ScheduleHelper.GetLessonClassroom(entity)
            };
        }

        public static OffsetRecordViewModel CreateRecordViewModel(this OffsetRecord entity)
        {
            return new OffsetRecordViewModel
            {
                Id = entity.Id,
                Day = entity.Day,
                Week = entity.Week,
                Lesson = entity.Lesson,
                NotParseRecord = entity.NotParseRecord,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom?.ToString(),
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline?.ToString(),
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer?.ToString(),
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup?.ToString()
            };
        }

        public static OffsetRecordShortViewModel CreateRecordShortViewModel(this OffsetRecord entity)
        {
            return new OffsetRecordShortViewModel
            {
                Id = entity.Id,
                Week = entity.Week,
                Day = entity.Day,
                Lesson = entity.Lesson,
                LessonLecturer = ScheduleHelper.GetLessonLecturer(entity),
                LessonDiscipline = ScheduleHelper.GetLessonDiscipline(entity),
                LessonGroup = ScheduleHelper.GetLessonGroup(entity),
                LessonClassroom = ScheduleHelper.GetLessonClassroom(entity)
            };
        }

        public static ExaminationRecordViewModel CreateRecordViewModel(this ExaminationRecord entity)
        {
            return new ExaminationRecordViewModel
            {
                Id = entity.Id,
                DateConsultation = entity.DateConsultation,
                DateExamination = entity.DateExamination,
                NotParseRecord = entity.NotParseRecord,
                LessonClassroom = entity.LessonClassroom,
                LessonConsultationClassroom = entity.LessonConsultationClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom?.ToString(),
                ConsultationClassroomId = entity.ConsultationClassroomId,
                ConsultationClassroom = entity.ConsultationClassroomId?.ToString(),
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline?.ToString(),
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer?.ToString(),
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup?.ToString()
            };
        }

        public static ExaminationRecordShortViewModel CreateRecordShortViewModel(this ExaminationRecord entity)
        {
            return new ExaminationRecordShortViewModel
            {
                Id = entity.Id,
                DateConsultation = entity.DateConsultation,
                DateExamination = entity.DateExamination,
                LessonLecturer = ScheduleHelper.GetLessonLecturer(entity),
                LessonDiscipline = ScheduleHelper.GetLessonDiscipline(entity),
                LessonGroup = ScheduleHelper.GetLessonGroup(entity),
                LessonClassroom = ScheduleHelper.GetLessonClassroom(entity),
                LessonConsultationClassroom = ScheduleHelper.GetLessonConsultationClassroom(entity)
            };
        }

        public static ConsultationRecordViewModel CreateRecordViewModel(this ConsultationRecord entity)
        {
            return new ConsultationRecordViewModel
            {
                Id = entity.Id,
                DateConsultation = entity.DateConsultation,
                NotParseRecord = entity.NotParseRecord,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom?.ToString(),
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline?.ToString(),
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer?.ToString(),
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup?.ToString()
            };
        }

        public static ConsultationRecordShortViewModel CreateRecordShortViewModel(this ConsultationRecord entity)
        {
            return new ConsultationRecordShortViewModel
            {
                Id = entity.Id,
                DateConsultation = entity.DateConsultation,
                LessonLecturer = ScheduleHelper.GetLessonLecturer(entity),
                LessonDiscipline = ScheduleHelper.GetLessonDiscipline(entity),
                LessonGroup = ScheduleHelper.GetLessonGroup(entity),
                LessonClassroom = ScheduleHelper.GetLessonClassroom(entity)
            };
        }

        public static ScheduleLessonTimeViewModel CreateViewModel(this ScheduleLessonTime entity)
        {
            return new ScheduleLessonTimeViewModel
            {
                Id = entity.Id,
                Text = string.Format("{0}{1}{2} - {3}", entity.Title, Environment.NewLine, entity.DateBeginLesson.ToShortTimeString(),
                entity.DateEndLesson.ToShortTimeString()),
                Title = entity.Title,
                Order = entity.Order,
                TimeBeginLesson = entity.DateBeginLesson.ToShortTimeString(),
                TimeEndLesson = entity.DateEndLesson.ToShortTimeString(),
                DateBeginLesson = entity.DateBeginLesson,
                DateEndLesson = entity.DateEndLesson
            };
        }
    }
}