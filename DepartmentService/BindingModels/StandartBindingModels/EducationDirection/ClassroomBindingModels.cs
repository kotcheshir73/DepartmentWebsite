using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class ClassroomGetBindingModel : PageSettingBinidingModel
	{
		public Guid Id { get; set; }

        public bool? NotUseInSchedule { get; set; }
    }

    public class ClassroomSetBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string Number { get; set; }

        [Required(ErrorMessage = "required")]
        public string ClassroomType { get; set; }

        [Required(ErrorMessage = "required")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "required")]
        public bool NotUseInSchedule { get; set; }
    }
}
