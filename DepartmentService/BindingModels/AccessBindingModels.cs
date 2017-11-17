using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class AccessGetBindingModel
	{
		public long? Id { get; set; }

		public long? RoleId { get; set; }
		
		public string Operation { get; set; }
	}

	public class AccessRecordBindingModel
	{
		public long Id { get; set; }

		public long RoleId { get; set; }

		[Required(ErrorMessage = "required")]
		public string Operation { get; set; }

		public string AccessType { get; set; }
	}
}
