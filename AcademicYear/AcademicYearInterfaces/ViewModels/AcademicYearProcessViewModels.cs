using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class AcademicPlanRecordForDiciplinePageViewModel : PageSettingListViewModel<AcademicPlanRecordForDiciplineViewModel> { }

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