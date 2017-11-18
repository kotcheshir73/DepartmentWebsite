using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class EducationDirectionGetBindingModel
    {
        public long Id { get; set; }

		public int? PageNumber { get; set; }

		public int? PageSize { get; set; }
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
