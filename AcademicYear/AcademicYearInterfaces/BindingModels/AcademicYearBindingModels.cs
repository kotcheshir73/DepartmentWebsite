using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class AcademicYearGetBindingModel : PageSettingGetBinidingModel { }

	public class AcademicYearSetBindingModel : PageSettingSetBinidingModel
	{
		[Required(ErrorMessage = "required")]
		public string Title { get; set; }
	}
}