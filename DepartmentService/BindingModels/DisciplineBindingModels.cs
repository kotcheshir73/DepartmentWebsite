﻿using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class DisciplineGetBindingModel
	{
		public long Id { get; set; }

		public long? DisciplineBlockId { get; set; }
	}

	public class DisciplineRecordBindingModel
	{
		public long Id { get; set; }

		public long DisciplineBlockId { get; set; }

		[Required(ErrorMessage = "required")]
		public string DisciplineName { get; set; }
	}

	public class DisciplineBlockGetBindingModel
	{
		public long Id { get; set; }
	}

	public class DisciplineBlockRecordBindingModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string Title { get; set; }
	}
}
