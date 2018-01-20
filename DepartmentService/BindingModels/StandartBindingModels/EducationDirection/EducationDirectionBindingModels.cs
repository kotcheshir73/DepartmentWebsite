using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class EducationDirectionGetBindingModel : PageSettingBinidingModel
	{
        public Guid? Id { get; set; }
	}

    public class EducationDirectionRecordBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string Cipher { get; set; }

        [Required(ErrorMessage = "required")]
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}
