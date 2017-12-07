using DepartmentService.Enums;

namespace DepartmentService.ViewModels
{
    public class AcademicPlanRecordForDiciplinePageViewModel : PageViewModel<AcademicPlanRecordForDiciplineViewModel> { }

    public class AcademicPlanRecordForDiciplineViewModel
    {
        public long Id { get; set; }

        public long AcademicPlanId { get; set; }

        public long DisciplineId { get; set; }

        public string EducationDirectionCipher { get; set; }

        public string Semesters { get; set; }

        public string Disciplne { get; set; }

        public long KindOfLoadId { get; set; }

        public string KindOfLoad { get; set; }

        public string Semester { get; set; }

        public int Hours { get; set; }
    }

    public class ScheduleRecordsForDisciplinePageViewModel : PageViewModel<ScheduleRecordsForDisciplineViewModel> { }

    public class ScheduleRecordsForDisciplineViewModel : ScheduleRecordShortViewModels
    {
        public ScheduleRecordTypeForDiscipline Type {get; set;}

        public string LessonType { get; set; }

        public string Date { get; set; }        
    }
}
