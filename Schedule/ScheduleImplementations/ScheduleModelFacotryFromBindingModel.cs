using Enums;
using Models;
using Models.AcademicYearData;
using Models.Schedule;
using ScheduleInterfaces.BindingModels;
using System;

namespace ScheduleImplementations
{
    public static class ScheduleModelFacotryFromBindingModel
    {
        private static void CreateScheduleRecord(ScheduleSetBindingModel model, ScheduleRecord entity = null)
        {
            if (!string.IsNullOrEmpty(model.LessonClassroom))
            {
                entity.LessonClassroom = model.LessonClassroom;
            }
            if (!string.IsNullOrEmpty(model.LessonDiscipline))
            {
                entity.LessonDiscipline = model.LessonDiscipline;
            }
            if (!string.IsNullOrEmpty(model.LessonLecturer))
            {
                entity.LessonLecturer = model.LessonLecturer;
            }
            if (!string.IsNullOrEmpty(model.LessonGroup))
            {
                entity.LessonGroup = model.LessonGroup;
            }
            if (model.ClassroomId.HasValue)
            {
                entity.ClassroomId = model.ClassroomId;
            }
            if (model.DisciplineId.HasValue)
            {
                entity.DisciplineId = model.DisciplineId;
            }
            if (model.LecturerId.HasValue)
            {
                entity.LecturerId = model.LecturerId;
            }
            if (model.StudentGroupId.HasValue)
            {
                entity.StudentGroupId = model.StudentGroupId;
            }
        }

        public static SemesterRecord CreateSemesterRecord(SemesterRecordRecordBindingModel model, SemesterRecord entity = null, SeasonDates seasonDate = null)
        {
            if (entity == null)
            {
                entity = new SemesterRecord()
                {
                    Week = model.Week,
                    Day = model.Day,
                    Lesson = model.Lesson,
                    SeasonDatesId = seasonDate.Id
                };
            }
            if (model.LessonType != LessonTypes.нд.ToString())
            {
                entity.LessonType = (LessonTypes)Enum.Parse(typeof(LessonTypes), model.LessonType);
            }
            entity.IsFirstHalfSemester = model.IsFirstHalfSemester;
            entity.IsStreaming = model.IsStreaming;
            entity.IsSubgroup = model.IsSubgroup;
            entity.NotParseRecord = model.NotParseRecord;
            CreateScheduleRecord(model, entity);

            return entity;
        }

        public static OffsetRecord CreateOffsetRecord(OffsetRecordRecordBindingModel model, OffsetRecord entity = null, SeasonDates seasonDate = null)
        {
            if (entity == null)
            {
                entity = new OffsetRecord()
                {
                    Week = model.Week,
                    Day = model.Day,
                    Lesson = model.Lesson,
                    NotParseRecord = model.NotParseRecord,
                    SeasonDatesId = seasonDate.Id
                };
            }
            CreateScheduleRecord(model, entity);

            return entity;
        }

        public static ExaminationRecord CreateExaminationRecord(ExaminationRecordRecordBindingModel model, ExaminationRecord entity = null, SeasonDates seasonDate = null)
        {
            if (entity == null)
            {
                entity = new ExaminationRecord()
                {
                    DateConsultation = model.DateConsultation,
                    DateExamination = model.DateExamination,
                    NotParseRecord = model.NotParseRecord,
                    SeasonDatesId = seasonDate.Id,
                    ConsultationClassroomId = model.ConsultationClassroomId,
                    LessonConsultationClassroom = model.LessonConsultationClassroom
                };
            }
            CreateScheduleRecord(model, entity);

            return entity;
        }

        public static ConsultationRecord CreateConsultationRecord(ConsultationRecordRecordBindingModel model, ConsultationRecord entity = null, SeasonDates seasonDate = null)
        {
            if (entity == null)
            {
                entity = new ConsultationRecord()
                {
                    DateConsultation = model.DateConsultation,
                    NotParseRecord = model.NotParseRecord,
                    SeasonDatesId = seasonDate.Id
                };
            }
            CreateScheduleRecord(model, entity);

            return entity;
        }

        public static ScheduleLessonTime CreateScheduleLessonTime(ScheduleLessonTimeSetBindingModel model, ScheduleLessonTime entity = null)
        {
            if (entity == null)
            {
                entity = new ScheduleLessonTime();
            }
            entity.Title = model.Title;
            entity.Order = model.Order;
            entity.DateBeginLesson = model.DateBeginLesson;
            entity.DateEndLesson = model.DateEndLesson;

            return entity;
        }

        public static StreamingLesson CreateStreamingLesson(StreamingLessonSetBindingModel model, StreamingLesson entity = null)
        {
            if (entity == null)
            {
                entity = new StreamingLesson();
            }
            entity.IncomingGroups = model.IncomingGroups;
            entity.StreamName = model.StreamName;

            return entity;
        }
    }
}