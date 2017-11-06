using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class RoleGetBindingModel
	{
		public long Id { get; set; }
	}

	public class RoleRecordBindingModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string RoleName { get; set; }
	}
}
