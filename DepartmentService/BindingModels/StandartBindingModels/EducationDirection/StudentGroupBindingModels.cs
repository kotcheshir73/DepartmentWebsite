using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class StudentGroupGetBindingModel : PageSettingBinidingModel
	{
        public long? Id { get; set; }

		public string GroupName { get; set; }
	}

	public class StudentGroupRecordBindingModel
    {
        public long Id { get; set; }

        public long EducationDirectionId { get; set; }

        [Required(ErrorMessage = "required")]
        public string GroupName { get; set; }
        
        public int Course { get; set; }

        public string StewardId { get; set; }

        public long? CuratorId { get; set; }
	}
}
