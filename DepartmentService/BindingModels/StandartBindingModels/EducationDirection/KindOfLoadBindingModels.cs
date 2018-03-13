using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class KindOfLoadGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }
	}

	public class KindOfLoadRecordBindingModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "required")]
		public string KindOfLoadName { get; set; }

        /*[Required(ErrorMessage = "required")]
		public string KindOfLoadType { get; set; }*/

        [Required(ErrorMessage = "required")]
        public string AttributeName { get; set; }
    }
}
