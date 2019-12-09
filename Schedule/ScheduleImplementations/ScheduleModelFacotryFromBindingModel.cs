using Models;
using Models.AcademicYearData;
using Models.Schedule;
using ScheduleInterfaces.BindingModels;

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

        public static SemesterRecord CreateRecord(this SemesterRecordSetBindingModel model, SemesterRecord entity = null)
        {
            if (entity == null)
            {
                entity = new SemesterRecord()
                {
                    Week = model.Week,
                    Day = model.Day,
                    Lesson = model.Lesson,
                    SeasonDatesId = model.SeasonDatesId.Value
                };
            }
            entity.LessonType = model.LessonType;
            entity.IsFirstHalfSemester = model.IsFirstHalfSemester;
            entity.NotParseRecord = model.NotParseRecord;
            CreateScheduleRecord(model, entity);

            return entity;
        }

        public static OffsetRecord CreateRecord(this OffsetRecordSetBindingModel model, OffsetRecord entity = null)
        {
            if (entity == null)
            {
                entity = new OffsetRecord()
                {
                    Week = model.Week,
                    Day = model.Day,
                    Lesson = model.Lesson,
                    NotParseRecord = model.NotParseRecord,
                    SeasonDatesId = model.SeasonDatesId.Value
                };
            }
            CreateScheduleRecord(model, entity);

            return entity;
        }

        public static ExaminationRecord CreateRecord(this ExaminationRecordSetBindingModel model, ExaminationRecord entity = null)
        {
            if (entity == null)
            {
                entity = new ExaminationRecord()
                {
                    NotParseRecord = model.NotParseRecord,
                    SeasonDatesId = model.SeasonDatesId.Value
                };
            }
            entity.DateExamination = model.DateExamination;
            entity.DateConsultation = model.DateConsultation;
            entity.ConsultationClassroomId = model.ConsultationClassroomId;
            entity.LessonConsultationClassroom = model.LessonConsultationClassroom;
            CreateScheduleRecord(model, entity);

            return entity;
        }

        public static ConsultationRecord CreateRecord(this ConsultationRecordSetBindingModel model, ConsultationRecord entity = null)
        {
            if (entity == null)
            {
                entity = new ConsultationRecord()
                {
                    NotParseRecord = model.NotParseRecord,
                    SeasonDatesId = model.SeasonDatesId.Value
                };
            }
            entity.DateConsultation = model.DateConsultation;
            CreateScheduleRecord(model, entity);

            return entity;
        }

        public static ScheduleLessonTime CreateEntity(this ScheduleLessonTimeSetBindingModel model, ScheduleLessonTime entity = null)
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
    }
}