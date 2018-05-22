using DepartmentModel.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class StudentGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public string NumberOfBook { get; set; }

        public Guid? StudentGroupId { get; set; }

		public StudentState? StudentStatus { get; set; }
    }

    public class StudentSetBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string NumberOfBook { get; set; }

        [Required(ErrorMessage = "required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "required")]
        public string LastName { get; set; }

        public string Patronymic { get; set; }

		[Required(ErrorMessage = "required")]
		public string Email { get; set; }

		public string Description { get; set; }

        [Required(ErrorMessage = "required")]
        public string StudentStatus { get; set; }

        public Guid StudentGroupId { get; set; }

        public byte[] Photo { get; set; }

        public bool IsSteward { get; set; }
    }
}
