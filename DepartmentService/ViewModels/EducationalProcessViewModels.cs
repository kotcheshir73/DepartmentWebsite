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

        //public Guid KindOfLoadId { get; set; }

        public string EducationDirectionCipher { get; set; }

        public string Semesters { get; set; }

        public string Disciplne { get; set; }

        //public string KindOfLoad { get; set; }

        public string Semester { get; set; }

        //public int Hours { get; set; }

        public int Zet { get; set; }
    }

    public class ScheduleRecordsForDisciplinePageViewModel : PageViewModel<ScheduleRecordsForDisciplineViewModel> { }

    public class ScheduleRecordsForDisciplineViewModel : ScheduleRecordShortViewModels
    {
        public ScheduleRecordTypeForDiscipline Type {get; set;}

        public string LessonType { get; set; }

        public string Date { get; set; }        
    }
}
