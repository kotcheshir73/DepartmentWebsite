using Enums;
using LearningProgressInterfaces.BindingModels;
using Models.LearningProgress;
using System;

namespace LearningProgressImplementations
{
    public static class LearningProgressModelFacotryFromBindingModel
	{
        public static DisciplineLesson CreateDisciplineLesson(DisciplineLessonRecordBindingModel model, DisciplineLesson entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineLesson();
            }
            entity.AcademicYearId = model.AcademicYearId;
            entity.DisciplineId = model.DisciplineId;
            entity.EducationDirectionId = model.EducationDirectionId;
            entity.TimeNormId = model.TimeNormId;
            entity.Semester = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Order = model.Order;
            entity.DisciplineLessonFile = model.DisciplineLessonFile;
            entity.Date = model.Date;
            entity.CountOfPairs = model.CountOfPairs;

            return entity;
        }

        public static DisciplineLessonTask CreateDisciplineLessonTask(DisciplineLessonTaskRecordBindingModel model, DisciplineLessonTask entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineLessonTask();
            }
            entity.DisciplineLessonId = model.DisciplineLessonId;
            entity.Order = model.Order;
            entity.Description = model.Description;
            entity.Image = model.Image;
            entity.IsNecessarily = model.IsNecessarily;
            entity.MaxBall = model.MaxBall;
            entity.Task = model.Task;

            return entity;
        }

        public static DisciplineLessonTaskVariant CreateDisciplineLessonTaskVariant(DisciplineLessonTaskVariantRecordBindingModel model, DisciplineLessonTaskVariant entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineLessonTaskVariant();
            }
            entity.DisciplineLessonTaskId = model.DisciplineLessonTaskId;
            entity.VariantNumber = model.VariantNumber;
            entity.VariantTask = model.VariantTask;
            entity.Order = model.Order;

            return entity;
        }

        public static DisciplineStudentRecord CreateDisciplineStudentRecord(DisciplineStudentRecordSetBindingModel model, DisciplineStudentRecord entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineStudentRecord();
            }
            entity.DisciplineId = model.DisciplineId;
            entity.StudentId = model.StudentId;
            entity.Semester = (Semesters)Enum.Parse(typeof(Semesters), model.Semester);
            entity.Variant = model.Variant;
            entity.SubGroup = model.SubGroup;

            return entity;
        }

        public static DisciplineLessonConducted CreateDisciplineLessonConducted(DisciplineLessonConductedSetBindingModel model, DisciplineLessonConducted entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineLessonConducted();
            }
            entity.DisciplineLessonId = model.DisciplineLessonId;
            entity.StudentGroupId = model.StudentGroupId;
            entity.DateCreate = model.Date;
            entity.Subgroup = model.Subgroup;

            return entity;
        }

        public static DisciplineLessonConductedStudent CreateDisciplineLessonConductedStudent(DisciplineLessonConductedStudentSetBindingModel model, DisciplineLessonConductedStudent entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineLessonConductedStudent();
            }
            entity.DisciplineLessonConductedId = model.DisciplineLessonConductedId;
            entity.StudentId = model.StudentId;
            entity.Status = (DisciplineLessonStudentStatus)Enum.Parse(typeof(DisciplineLessonStudentStatus), model.Status);
            entity.Comment = model.Comment;
            entity.Ball = model.Ball;

            return entity;
        }

        public static DisciplineLessonTaskStudentAccept CreateDisciplineLessonTaskStudentAccept(DisciplineLessonTaskStudentAcceptSetBindingModel model, DisciplineLessonTaskStudentAccept entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineLessonTaskStudentAccept();
            }
            entity.DisciplineLessonTaskId = model.DisciplineLessonTaskId;
            entity.StudentId = model.StudentId;
            entity.Result = (DisciplineLessonTaskStudentResult)Enum.Parse(typeof(DisciplineLessonTaskStudentResult), model.Result);
            entity.Task = model.Task;
            entity.DateAccept = model.DateAccept;
            entity.Score = model.Score;
            entity.Comment = model.Comment;
            entity.Log = model.Log;

            return entity;
        }
    }
}