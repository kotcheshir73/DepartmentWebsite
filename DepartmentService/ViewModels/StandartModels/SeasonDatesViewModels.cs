namespace DepartmentService.ViewModels
{
	public class SeasonDatesPageViewModel : PageViewModel<SeasonDatesViewModel> { }

	public class SeasonDatesViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string DateBeginSemester { get; set; }

        public string DateEndSemester { get; set; }

        public string DateBeginOffset { get; set; }

        public string DateEndOffset { get; set; }

        public string DateBeginExamination { get; set; }

        public string DateEndExamination { get; set; }

        public string DateBeginPractice { get; set; }

        public string DateEndPractice { get; set; }
    }
}
