using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace AuthenticationInterfaces.BindingModels
{
    public class UserGetBindingModel : PageSettingGetBinidingModel
	{
		public bool? IsBanned { get; set; }

        public string RoleType { get; set; }

		public List<Guid> LecturerIds { get; set; } 
    }

	public class UserSetBindingModel : PageSettingSetBinidingModel
	{
		[Required(ErrorMessage = "required")]
		public string Login { get; set; }
		
		public string Password { get; set; }

		public byte[] Avatar { get; set; }

		public Guid? StudentId { get; set; }

		public Guid? LecturerId { get; set; }

        public bool IsBanned { get; set; }
	}
}