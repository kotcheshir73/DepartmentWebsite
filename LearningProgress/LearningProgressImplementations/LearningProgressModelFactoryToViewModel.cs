using LearningProgressInterfaces.ViewModels;
using Models.LearningProgress;
using System.Linq;

namespace LearningProgressImplementations
{
    public static class LearningProgressModelFactoryToViewModel
    {
        public static DisciplineLessonViewModel CreateDisciplineLessonViewModel(DisciplineLesson entity)
        {
            return new DisciplineLessonViewModel
            {
                Id = entity.Id,
                AcademicYearId = entity.AcademicYearId,
                DisciplineId = entity.DisciplineId,
                EducationDirectionId = entity.EducationDirectionId,
                TimeNormId = entity.TimeNormId,
                AcademicYear = entity.AcademicYear.Title,
                Discipline = entity.Discipline.DisciplineName,
                EducationDirection = entity.EducationDirection.ShortName,
                TimeNorm = entity.TimeNorm.TimeNormName,
                Semester = entity.Semester,
                Title = entity.Title,
                Description = entity.Description,
                Order = entity.Order,
                DisciplineLessonFile = entity.DisciplineLessonFile,
                CountOfPairs = entity.CountOfPairs,
                CountTasks = entity.DisciplineLessonTasks.Count,
                Date = entity.Date
            };
        }

        public static DisciplineLessonTaskViewModel CreateDisciplineLessonTaskViewModel(DisciplineLessonTask entity)
        {
            return new DisciplineLessonTaskViewModel
            {
                Id = entity.Id,
                DisciplineLessonId = entity.DisciplineLessonId,
                DisciplineLessonTitle = entity.DisciplineLesson.Title,
                Task = entity.Task,
                Description = entity.Description,
                MaxBall = entity.MaxBall,
                Order = entity.Order,
                Image = entity.Image,
                IsNecessarily = entity.IsNecessarily
            };
        }

        public static DisciplineLessonTaskVariantViewModel CreateDisciplineLessonTaskVariantViewModel(DisciplineLessonTaskVariant entity)
        {
            return new DisciplineLessonTaskVariantViewModel
            {
                Id = entity.Id,
                DisciplineLessonTaskId = entity.DisciplineLessonTaskId,
                DisciplineLessonTaskTask = entity.DisciplineLessonTask.Task,
                VariantNumber = entity.VariantNumber,
                VariantTask = entity.VariantTask,
                Order = entity.Order
            };
        }

        public static DisciplineStudentRecordViewModel CreateDisciplineStudentRecordViewModel(DisciplineStudentRecord entity)
        {
            return new DisciplineStudentRecordViewModel
            {
                Id = entity.Id,
                DisciplineId = entity.DisciplineId,
                StudentGroupId = entity.Student.StudentGroupId,
                StudentId = entity.StudentId,
                Discipline = entity.Discipline.DisciplineName,
                StudentGroup = entity.Student.StudentGroup.GroupName,
                Student = string.Format("{0} {1}", entity.Student.LastName, entity.Student.FirstName),
                Semester = entity.Semester,
                Variant = entity.Variant,
                SubGroup = entity.SubGroup
            };
        }

        public static DisciplineLessonConductedViewModel CreateDisciplineLessonConductedViewModel(DisciplineLessonConducted entity)
        {
            return new DisciplineLessonConductedViewModel
            {
                Id = entity.Id,
                Semester = entity.DisciplineLesson.Semester.ToString(),
                DisciplineLessonId = entity.DisciplineLessonId,
                StudentGroupId = entity.StudentGroupId,
                DisciplineLesson = entity.DisciplineLesson.Title,
                StudentGroup = entity.StudentGroup.GroupName,
                Date = entity.DateCreate,
                Subgroup = entity.Subgroup
            };
        }

        public static DisciplineLessonConductedStudentViewModel CreateDisciplineLessonConductedStudentViewModel(DisciplineLessonConductedStudent entity)
        {
            return new DisciplineLessonConductedStudentViewModel
            {
                Id = entity.Id,
                DisciplineLessonConductedId = entity.DisciplineLessonConductedId,
                StudentId = entity.StudentId,
                DisciplineLesson = string.Format("{0} от {1}", entity.DisciplineLessonConducted.DisciplineLesson.Title, entity.DisciplineLessonConducted.DateCreate.ToShortDateString()),
                Student = string.Format("{0} {1}", entity.Student.LastName, entity.Student.FirstName),
                Comment = entity.Comment,
                Status = entity.Status,
                Ball = entity.Ball
            };
        }

        public static DisciplineLessonTaskStudentAcceptViewModel CreateDisciplineLessonTaskStudentAcceptViewModel(DisciplineLessonTaskStudentAccept entity)
        {
            return new DisciplineLessonTaskStudentAcceptViewModel
            {
                Id = entity.Id,
                DisciplineLessonTaskId = entity.DisciplineLessonTaskId,
                StudentId = entity.StudentId,
                DisciplineLessonTask = entity.DisciplineLessonTask.Task,
                Student = string.Format("{0} {1}", entity.Student.LastName, entity.Student.FirstName),
                Result = entity.Result,
                Task = entity.Task,
                DateAccept = entity.DateAccept,
                Score = entity.Score,
                Comment = entity.Comment,
                Log = entity.Log
            };
        }
    }
}