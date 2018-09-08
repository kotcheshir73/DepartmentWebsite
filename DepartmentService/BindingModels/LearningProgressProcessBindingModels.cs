using System;

namespace DepartmentService.BindingModels
{
    public class LearningProcessDisciplineBindingModel
    {
        public Guid UserId { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid EducationDirectionId { get; set; }
    }


    public class LearningProcessDisciplineDetailBindingModel
    {
        public Guid UserId { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid EducationDirectionId { get; set; }
    }
}
