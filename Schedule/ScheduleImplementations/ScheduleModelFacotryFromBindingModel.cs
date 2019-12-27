using Enums;
using Models;
using Models.Schedule;
using ScheduleImplementations.Helpers;
using ScheduleInterfaces.BindingModels;

namespace ScheduleImplementations
{
    public static class ScheduleModelFacotryFromBindingModel
    {
        private static void CreateScheduleRecord(ScheduleSetBindingModel model, ScheduleRecord entity = null)
        {
            entity.NotParseRecord = model.NotParseRecord;
            entity.ScheduleDate = model.ScheduleDate;

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
            if (!string.IsNullOrEmpty(model.LessonStudentGroup))
            {
                entity.LessonStudentGroup = model.LessonStudentGroup;
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
                entity = new SemesterRecord();
            }
            CreateScheduleRecord(model, entity);

            entity.LessonType = model.LessonType;
            entity.ScheduleDate = ScheduleHelper.GetDateWithTime(model.ScheduleDate, model.Week, model.Day, model.Lesson);

            return entity;
        }

        public static OffsetRecord CreateRecord(this OffsetRecordSetBindingModel model, OffsetRecord entity = null)
        {
            if (entity == null)
            {
                entity = new OffsetRecord();
            }
            CreateScheduleRecord(model, entity);

            entity.LessonType = LessonTypes.зачет;
            entity.ScheduleDate = ScheduleHelper.GetDateWithTime(model.ScheduleDate, model.Lesson);

            return entity;
        }

        public static ExaminationRecord CreateRecord(this ExaminationRecordSetBindingModel model, ExaminationRecord entity = null)
        {
            if (entity == null)
            {
                entity = new ExaminationRecord();
            }
            CreateScheduleRecord(model, entity);

            entity.LessonType = LessonTypes.экзамен;
            entity.DateConsultation = model.DateConsultation;
            entity.ConsultationClassroomId = model.ConsultationClassroomId;
            entity.LessonConsultationClassroom = model.LessonConsultationClassroom;

            return entity;
        }

        public static ConsultationRecord CreateRecord(this ConsultationRecordSetBindingModel model, ConsultationRecord entity = null)
        {
            if (entity == null)
            {
                entity = new ConsultationRecord();
            }
            CreateScheduleRecord(model, entity);

            entity.LessonType = LessonTypes.конс;
            entity.ConsultationTime = model.ConsultationTime;

            return entity;
        }
    }
}