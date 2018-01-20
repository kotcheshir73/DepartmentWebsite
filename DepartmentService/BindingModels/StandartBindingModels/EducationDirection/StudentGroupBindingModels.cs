using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class StudentGroupGetBindingModel : PageSettingBinidingModel
	{
        public Guid? Id { get; set; }

		public string GroupName { get; set; }
	}

	public class StudentGroupRecordBindingModel
    {
        public Guid Id { get; set; }

        public Guid EducationDirectionId { get; set; }

        [Required(ErrorMessage = "required")]
        public string GroupName { get; set; }
        
        public int Course { get; set; }

        public string StewardName { get; set; }

        public Guid? CuratorId { get; set; }
	}
}
