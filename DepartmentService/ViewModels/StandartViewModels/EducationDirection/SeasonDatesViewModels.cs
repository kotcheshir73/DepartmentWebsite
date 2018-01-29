﻿using System;

namespace DepartmentService.ViewModels
{
	public class SeasonDatesPageViewModel : PageViewModel<SeasonDatesViewModel> { }

	public class SeasonDatesViewModel
    {
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public string AcademicYear { get; set; }

        public string Title { get; set; }
        
        public string DateBeginFirstHalfSemester { get; set; }
        
        public string DateEndFirstHalfSemester { get; set; }
        
        public string DateBeginSecondHalfSemester { get; set; }
        
        public string DateEndSecondHalfSemester { get; set; }

        public string DateBeginOffset { get; set; }

        public string DateEndOffset { get; set; }

        public string DateBeginExamination { get; set; }

        public string DateEndExamination { get; set; }

        public string DateBeginPractice { get; set; }

        public string DateEndPractice { get; set; }
    }
}
