using System.ComponentModel.DataAnnotations;

namespace Interfaces.BindingModels
{
    public class ClassroomGetBindingModel : PageSettingGetBinidingModel
	{
        public bool? NotUseInSchedule { get; set; }
    }

    public class ClassroomSetBindingModel : PageSettingSetBinidingModel
    {
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