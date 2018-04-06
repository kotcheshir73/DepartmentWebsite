using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class UserGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }

		public bool? IsBanned { get; set; }

        public string RoleType { get; set; }
    }

	public class UserRecordBindingModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string Login { get; set; }
		
		public string Password { get; set; }

		public byte[] Avatar { get; set; }

		public Guid? StudentId { get; set; }

		public Guid? LecturerId { get; set; }

        public bool IsBanned { get; set; }
	}
}
