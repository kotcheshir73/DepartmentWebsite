using System;
using System.ComponentModel.DataAnnotations;

namespace Interfaces.BindingModels
{
    public class StudentGroupGetBindingModel : PageSettingGetBinidingModel
	{
        public Guid? EducationDirectionId { get; set; }

        public string GroupName { get; set; }

        public string Course { get; set; }
    }

	public class StudentGroupSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid EducationDirectionId { get; set; }

        [Required(ErrorMessage = "required")]
        public string GroupName { get; set; }
        
        public int Course { get; set; }

        public Guid? CuratorId { get; set; }
	}
}