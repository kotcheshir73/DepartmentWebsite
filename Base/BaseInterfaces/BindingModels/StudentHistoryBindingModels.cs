using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace BaseInterfaces.BindingModels
{
    public class StudentHistoryGetBindingModel : PageSettingGetBinidingModel
	{
		public Guid? StudetnId { get; set; }
	}

	public class StudentHistorySetBindingModel : PageSettingSetBinidingModel
	{
		[Required(ErrorMessage = "required")]
		public Guid StudetnId { get; set; }

		public DateTime DateCreate { get; set; }

		[Required(ErrorMessage = "required")]
		public string TextMessage { get; set; }
	}
}