using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class AccessGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }

		public Guid? RoleId { get; set; }
		
		public string Operation { get; set; }
	}

	public class AccessSetBindingModel
	{
		public Guid Id { get; set; }

		public Guid RoleId { get; set; }

		[Required(ErrorMessage = "required")]
		public string Operation { get; set; }

		public string AccessType { get; set; }
	}
}
