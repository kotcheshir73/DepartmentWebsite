﻿using System;

namespace DepartmentService.ViewModels
{
	public class DisciplinePageViewModel : PageViewModel<DisciplineViewModel> { }

	public class DisciplineViewModel
	{
		public Guid Id { get; set; }

		public Guid DisciplineBlockId { get; set; }

        public Guid DisciplineParentId { get; set; }

        public bool IsParent { get; set; }

        public string DisciplineName { get; set; }

        public string DisciplineShortName { get; set; }

        public string DisciplineBlockTitle { get; set; }

        public string DisciplineBlueAsteriskName { get; set; }
    }
}
