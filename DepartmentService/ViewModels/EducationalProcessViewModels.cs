using DepartmentService.Enums;
using System;

namespace DepartmentService.ViewModels
{
    public class AcademicPlanRecordForDiciplinePageViewModel : PageViewModel<AcademicPlanRecordForDiciplineViewModel> { }

    public class AcademicPlanRecordForDiciplineViewModel
    {
        public Guid Id { get; set; }

        public Guid AcademicPlanId { get; set; }

        public Guid DisciplineId { get; set; }

        public string EducationDirectionShortName { get; set; }

        public string Semesters { get; set; }

        public string Disciplne { get; set; }

        public string Semester { get; set; }

        public int Zet { get; set; }
    }
}
