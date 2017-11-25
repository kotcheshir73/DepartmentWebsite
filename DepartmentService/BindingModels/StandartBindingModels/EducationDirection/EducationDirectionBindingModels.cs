using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class EducationDirectionGetBindingModel : PageSettingBinidingModel
	{
        public long? Id { get; set; }
	}

    public class EducationDirectionRecordBindingModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string Cipher { get; set; }

        [Required(ErrorMessage = "required")]
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}
