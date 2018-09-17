using DepartmentModel.Enums;
using System;
using System.Collections.Generic;

namespace DepartmentService.BindingModels
{
    public class LearningProcessDisciplineBindingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid EducationDirectionId { get; set; }

        public Guid UserId { get; set; }
    }

    public class LearningProcessDisciplineDetailBindingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid EducationDirectionId { get; set; }

        public Guid UserId { get; set; }
    }

    public class LearningProcessFormDisciplineLessonsBindingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid EducationDirectionId { get; set; }

        public Guid TimeNormId { get; set; }

        public string Semester { get; set; }

        public int CountLessons { get; set; }
    }

    public class LearningProcessFormDisciplineLessonTasksBindingModel
    {
        public Guid DisciplineLessonId { get; set; }

        public string TitleTemplate { get; set; }
        
        public List<string> Tasks { get; set; }

        public decimal? MaxBall { get; set; }

        public bool IsNecessarily { get; set; }
    }

    public class LearningProcessFormDisciplineLessonTaskVariantsBindingModel
    {
        public Guid DisciplineLessonTaskId { get; set; }

        public string VariantNumberTemplate { get; set; }

        public List<string> Variants { get; set; }
    }

    public class GetDisciplineLessonTaskVariants
    {
        public Guid DisciplineLessonId { get; set; }
    }

    public class GetDisiplineLessonTasksForDuplicate
    {
        public Guid DisciplineLessonTaskId { get; set; }
    }

    public class DuplicateDisiplineLessonTasks
    {
        public Guid DisciplineLessonTaskFromId { get; set; }

        public Guid DisciplineLessonTaskToId { get; set; }
    }

    public class GetDisiplineLessonsForDuplicate
    {
        public Guid DisciplineLessonId { get; set; }
    }

    public class DuplicateDisiplineLessons
    {
        public Guid DisciplineLessonFromId { get; set; }

        public Guid DisciplineLessonToId { get; set; }

        public bool CopyVariants { get; set; }
    }

    public class LearningProcessSemesterBindingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid EducationDirectionId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid UserId { get; set; }
    }

    public class LearningProcessStudentGroupBindingModel
    {
        public Guid EducationDirectionId { get; set; }

        public List<Semesters> Semesters { get; set; }
    }

    public class DisciplineStudentRecordsForFill
    {
        public Guid DisciplineId { get; set; }

        public Guid StudentGroupId { get; set; }

        public Semesters Semester { get; set; }
    }

    public class DisciplineLessonSubgroup
    {
        public Guid DisciplineId { get; set; }

        public Guid StudentGroupId { get; set; }

        public string Semester { get; set; }
    }

    public class DisciplineLessonConductedStudentsForFill
    {
        public Guid DisciplineLessonConductedId { get; set; }

        public Guid StudentGroupId { get; set; }
    }

    public class LessonConducteds
    {
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid StudentGroupId { get; set; }

        public Guid TimeNormId { get; set; }

        public string Semester { get; set; }
    }
}
