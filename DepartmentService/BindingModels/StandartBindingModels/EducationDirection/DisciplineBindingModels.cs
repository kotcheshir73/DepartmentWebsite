using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{

	public class DisciplineGetBindingModel : PageSettingBinidingModel
	{
		public Guid? Id { get; set; }

		public Guid? DisciplineBlockId { get; set; }
	}

	public class DisciplineSetBindingModel
	{
		public Guid Id { get; set; }

		public Guid DisciplineBlockId { get; set; }

        public Guid? DisciplineParentId { get; set; }
        
        public bool IsParent { get; set; }

        [Required(ErrorMessage = "required")]
		public string DisciplineName { get; set; }

        [Required(ErrorMessage = "required")]
        public string DisciplineShortName { get; set; }

        [Required(ErrorMessage = "required")]
        public string DisciplineBlueAsteriskName { get; set; }
    }
}
