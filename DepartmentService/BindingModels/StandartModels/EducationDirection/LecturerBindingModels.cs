using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class LecturerGetBindingModel : PageSettingBinidingModel
	{
		public long? Id { get; set; }
	}

	public class LecturerRecordBindingModel
	{
		public long Id { get; set; }

		public string FirstName { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "required")]
		public string Patronymic { get; set; }

		public string Abbreviation { get; set; }

		[Required(ErrorMessage = "required")]
		public DateTime DateBirth { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string Address { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string Email { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string MobileNumber { get; set; }
		
		public string HomeNumber { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string Post { get; set; }
		
		public string Rank { get; set; }
		
		public string Description { get; set; }
		
		public byte[] Photo { get; set; }
	}
}
