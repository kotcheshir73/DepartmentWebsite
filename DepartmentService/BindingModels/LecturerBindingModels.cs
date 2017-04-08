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
		
		[Required]
		public string LastName { get; set; }
		
		[Required]
		public string Patronymic { get; set; }
		
		[Required]
		public DateTime DateBirth { get; set; }
		
		[Required]
		public string Address { get; set; }
		
		[Required]
		public string Email { get; set; }
		
		[Required]
		public string MobileNumber { get; set; }
		
		[Required]
		public string HomeNumber { get; set; }
		
		[Required]
		public string Post { get; set; }
		
		[Required]
		public string Rank { get; set; }
		
		[Required]
		public string Description { get; set; }
		
		[Required]
		public byte[] Photo { get; set; }
	}
}
