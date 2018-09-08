using DepartmentModel.Enums;
using System;

namespace DepartmentService.ViewModels
{
    public class LearningProcessDisciplineViewModel
    {
        public Guid Id { get; set; }

        public string DisciplineName { get; set; }
    }

    public class LearningProcessDisciplineDetailViewModel
    {
        public Guid Id { get; set; }

        public string TimeNormName { get; set; }

        public string Info { get; set; }
    }
}
