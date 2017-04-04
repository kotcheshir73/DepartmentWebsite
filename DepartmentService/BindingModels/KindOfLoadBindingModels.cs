using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class KindOfLoadGetBindingModel
	{
		public long Id { get; set; }
	}

	public class KindOfLoadRecordBindingModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string KindOfLoadName { get; set; }

		[Required(ErrorMessage = "required")]
		public string KindOfLoadType { get; set; }
	}

	public class TimeNormGetBindingModel
	{
		public long Id { get; set; }
	}

	public class TimeNormRecordBindingModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string Title { get; set; }
		
		public long KindOfLoadId { get; set; }

		public long? ParentTimeNormId { get; set; }

		public decimal Hours { get; set; }
	}
}
