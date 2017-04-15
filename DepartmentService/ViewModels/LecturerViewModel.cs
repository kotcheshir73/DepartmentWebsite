using System;
using System.Drawing;

namespace DepartmentService.ViewModels
{
	public class LecturerViewModel
	{
		public long Id { get; set; }

		public string FirstName { get; set; }
		
		public string LastName { get; set; }
		
		public string Patronymic { get; set; }

		public  string Abbreviation { get; set; }

		public DateTime DateBirth { get; set; }
		
		public string Address { get; set; }
		
		public string Email { get; set; }
		
		public string MobileNumber { get; set; }
		
		public string HomeNumber { get; set; }
		
		public string Post { get; set; }
		
		public string Rank { get; set; }
		
		public string Description { get; set; }
		
		public Image Photo { get; set; }
	}
}
