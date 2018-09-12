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
}
