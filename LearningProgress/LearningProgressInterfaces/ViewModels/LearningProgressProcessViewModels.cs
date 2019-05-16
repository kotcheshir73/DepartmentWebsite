using System;

namespace LearningProgressInterfaces.ViewModels
{
    public class LearningProcessDisciplineViewModel
    {
        public Guid Id { get; set; }

        public string DisciplineName { get; set; }

        public override string ToString()
        {
            return DisciplineName;
        }
    }

    public class LearningProcessDisciplineDetailViewModel
    {
        public Guid Id { get; set; }

        public string TimeNormName { get; set; }

        public string Info { get; set; }

        public override string ToString()
        {
            return TimeNormName;
        }
    }

    public class LessonConductedViewModel
    {
        public string DisciplineLesson { get; set; }

        public string Student { get; set; }

        public string StatusBall { get; set; }

        public string Subgroup { get; set; }
    }
}