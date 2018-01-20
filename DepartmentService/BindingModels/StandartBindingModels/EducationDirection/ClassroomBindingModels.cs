using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class ClassroomGetBindingModel : PageSettingBinidingModel
	{
		public Guid Id { get; set; }
	}

    public class ClassroomRecordBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string Number { get; set; }

        [Required(ErrorMessage = "required")]
        public string ClassroomType { get; set; }

        [Required(ErrorMessage = "required")]
        public int Capacity { get; set; }
    }
}
