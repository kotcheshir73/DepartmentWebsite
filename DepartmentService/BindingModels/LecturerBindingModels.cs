using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
	public class LecturerGetBindingModel
	{
		public long Id { get; set; }
	}

	public class LecturerRecordBindingModel
	{
		public long Id { get; set; }

		public string FirstName { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string LastName { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string Patronymic { get; set; }
		
		[Required(ErrorMessage = "required")]
		public DateTime DateBirth { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string Address { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string Email { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string MobileNumber { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string HomeNumber { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string Post { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string Rank { get; set; }
		
		[Required(ErrorMessage = "required")]
		public string Description { get; set; }
		
		[Required(ErrorMessage = "required")]
		public byte[] Photo { get; set; }
	}
}
