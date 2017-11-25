using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class UserGetBindingModel : PageSettingBinidingModel
	{
		public long? Id { get; set; }

		public bool? IsBanned { get; set; }

		public long? RoleId { get; set; }
	}

	public class UserRecordBindingModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string Login { get; set; }
		
		public string Password { get; set; }

		public byte[] Avatar { get; set; }

		[Required(ErrorMessage = "required")]
		public long RoleId { get; set; }

		public long? StudentId { get; set; }

		public long? LecturerId { get; set; }

		public bool IsBanned { get; set; }
	}
}
