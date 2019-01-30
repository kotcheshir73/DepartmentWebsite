using ScheduleModels.Models;
using ScheduleServiceImplementations.Helpers;
using ScheduleServiceInterfaces.BindingModels;
using ScheduleServiceInterfaces.ViewModels;
using System;

namespace ScheduleServiceImplementations
{
    public static class ScheduleModelFactoryToViewModel
    {
        public static ScheduleRecordViewModel CreateScheduleRecordViewModel(ScheduleRecord entity)
        {
            return new ScheduleRecordViewModel
            {
                Id = entity.Id,
                NotParseRecord = entity.NotParseRecord,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom != null ? entity.Classroom.Number : "",
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline != null ? entity.Discipline.DisciplineName : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static SemesterRecordViewModel CreateSemesterRecordViewModel(SemesterRecord entity)
        {
            return new SemesterRecordViewModel
            {
                Id = entity.Id,
                Day = entity.Day,
                Week = entity.Week,
                Lesson = entity.Lesson,
                NotParseRecord = entity.NotParseRecord,
                IsFirstHalfSemester = entity.IsFirstHalfSemester,
                IsStreaming = entity.IsStreaming,
                IsSubgroup = entity.IsSubgroup,
                LessonClassroom = entity.LessonClassroom,
                LessonGroup = entity.LessonGroup,
                LessonDiscipline = entity.LessonDiscipline,
                LessonLecturer = entity.LessonLecturer,
                LessonType = entity.LessonType.ToString(),
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom != null ? entity.Classroom.Number : "",
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline != null ? entity.Discipline.DisciplineName : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static SemesterRecordShortViewModel CreateSemesterRecordShortViewModel(SemesterRecord entity, string groups)
        {
            return new SemesterRecordShortViewModel
            {
                Id = entity.Id,
                Week = entity.Week,
                Day = entity.Day,
                Lesson = entity.Lesson,
                LessonType = entity.LessonType.ToString(),
                IsStreaming = entity.IsStreaming,
                IsSubgroup = entity.IsSubgroup,
                LessonLecturer = ScheduleHelper.GetLessonLecturer(entity),
                LessonDiscipline = ScheduleHelper.GetLessonDiscipline(entity),
                LessonGroup = groups,
                LessonClassroom = ScheduleHelper.GetLessonClassroom(entity)
            };
        }

        public static OffsetRecordViewModel CreateOffsetRecordViewModel(OffsetRecord entity)
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
                Classroom = entity.Classroom != null ? entity.Classroom.Number : "",
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline != null ? entity.Discipline.DisciplineName : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static OffsetRecordShortViewModel CreateOffsetRecordShortViewModel(OffsetRecord entity)
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

        public static ExaminationRecordViewModel CreateExaminationRecordViewModel(ExaminationRecord entity)
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
                Classroom = entity.Classroom != null ? entity.Classroom.Number : "",
                ConsultationClassroomId = entity.ConsultationClassroomId,
                ConsultationClassroom = entity.ConsultationClassroomId != null ? entity.ConsultationClassroom.Number : "",
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline != null ? entity.Discipline.DisciplineName : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static ExaminationRecordShortViewModel CreateExaminationRecordShortViewModel(ExaminationRecord entity)
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

        public static ConsultationRecordViewModel CreateConsultationRecordViewModel(ConsultationRecord entity)
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
                Classroom = entity.Classroom != null ? entity.Classroom.Number : "",
                DisciplineId = entity.DisciplineId,
                Discipline = entity.Discipline != null ? entity.Discipline.DisciplineName : "",
                LecturerId = entity.LecturerId,
                Lecturer = entity.Lecturer != null ? entity.Lecturer.ToString() : "",
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup != null ? entity.StudentGroup.GroupName : ""
            };
        }

        public static ConsultationRecordShortViewModel CreateConsultationRecordShortViewModel(ConsultationRecord entity, ConsultationRecordRecordBindingModel model)
        {
            return new ConsultationRecordShortViewModel
            {
                Id = entity.Id,
                Week = model.Week.Value,
                Day = model.Day.Value,
                Lesson = model.Lesson.Value,
                DateConsultation = entity.DateConsultation,
                LessonLecturer = ScheduleHelper.GetLessonLecturer(entity),
                LessonDiscipline = ScheduleHelper.GetLessonDiscipline(entity),
                LessonGroup = ScheduleHelper.GetLessonGroup(entity),
                LessonClassroom = ScheduleHelper.GetLessonClassroom(entity)
            };
        }

        public static ScheduleLessonTimeViewModel CreateScheduleLessonTimeViewModel(ScheduleLessonTime entity)
        {
            string text = string.Format("{0}{1}{2} - {3}", entity.Title, Environment.NewLine, entity.DateBeginLesson.ToShortTimeString(),
                entity.DateEndLesson.ToShortTimeString());
            return new ScheduleLessonTimeViewModel
            {
                Id = entity.Id,
                Text = text,
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