using Enums;
using System;
using System.Collections.Generic;

namespace LearningProgressInterfaces.BindingModels
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

        public string Semester { get; set; }
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

        public double? MaxBall { get; set; }

        public bool IsNecessarily { get; set; }
    }

    public class LearningProcessFormDisciplineLessonTaskVariantsBindingModel
    {
        public Guid DisciplineLessonTaskId { get; set; }

        public string VariantNumberTemplate { get; set; }

        public List<string> Variants { get; set; }
    }

    public class GetDisciplineLessonTaskVariantsBindingModel
    {
        public Guid DisciplineLessonId { get; set; }
    }

    public class GetDisiplineLessonTasksForDuplicateBindingModel
    {
        public Guid DisciplineLessonTaskId { get; set; }
    }

    public class DuplicateDisiplineLessonTasksBindingModel
    {
        public Guid DisciplineLessonTaskFromId { get; set; }

        public Guid DisciplineLessonTaskToId { get; set; }
    }

    public class GetDisiplineLessonsForDuplicateBindingModel
    {
        public Guid DisciplineLessonId { get; set; }
    }

    public class DuplicateDisiplineLessonsBindingModel
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

    public class DisciplineStudentRecordsForFillBindingModel
    {
        public Guid DisciplineId { get; set; }

        public Guid StudentGroupId { get; set; }

        public Semesters Semester { get; set; }
    }

    public class DisciplineLessonSubgroupBindingModel
    {
        public Guid DisciplineId { get; set; }

        public Guid StudentGroupId { get; set; }

        public string Semester { get; set; }
    }

    public class DisciplineLessonConductedStudentsForFillBindingModel
    {
        public Guid DisciplineLessonConductedId { get; set; }

        public Guid StudentGroupId { get; set; }
    }

    public class LessonConductedsBindingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid StudentGroupId { get; set; }

        public Guid TimeNormId { get; set; }

        public string Semester { get; set; }
    }

    public class LearningProcessDisciplineLessonBindingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid EducationDirectionId { get; set; }

        public string Semester { get; set; }
    }

    public class DisciplineLessonTaskStudentAcceptForFormBindingModel
    {
        public Guid DisciplineLessonTaskId { get; set; }

        public Guid StudentGroupId { get; set; }

        public DateTime DateAccept { get; set; }
    }

    public class DisciplineLessonTaskStudentAcceptUpdateBindingModel
    {
        public Guid DisciplineLessonTaskStudentAcceptTaskId { get; set; }

        public string Task{ get; set; }

        public string Comment { get; set; }
    }

    public class DisciplineLessonTaskStudentAcceptForFillBindingModel
    {
        public Guid DisciplineLessonTaskId { get; set; }

        public Guid StudentGroupId { get; set; }
    }

    public class FullDisciplineLessonConductedBindingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid EducationDirectionId { get; set; }

        public Guid StudentGroupId { get; set; }

        public Guid TimeNormId { get; set; }

        public string Semester { get; set; }
    }
}