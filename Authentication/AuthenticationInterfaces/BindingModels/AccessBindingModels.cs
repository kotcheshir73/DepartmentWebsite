using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace AuthenticationInterfaces.BindingModels
{
    public class AccessGetBindingModel : PageSettingGetBinidingModel
	{
		public Guid? RoleId { get; set; }
		
		public string Operation { get; set; }
	}

	public class AccessSetBindingModel : PageSettingSetBinidingModel
	{
		public Guid RoleId { get; set; }

		[Required(ErrorMessage = "required")]
		public string Operation { get; set; }

		public string AccessType { get; set; }
	}
}