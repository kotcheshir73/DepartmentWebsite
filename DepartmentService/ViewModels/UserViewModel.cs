using System;
using System.Drawing;

namespace DepartmentService.ViewModels
{
	public class UserViewModel
	{
		public long Id { get; set; }

		public long RoleId { get; set; }

		public string RoleName { get; set; }

		public long? StudentId { get; set; }

		public long? LecturerId { get; set; }
		
		public string Login { get; set; }
		
		public Image Avatar { get; set; }
		
		public DateTime? DateLastVisit { get; set; }
		
		public DateTime? DateBanned { get; set; }
		
		public bool IsBanned { get; set; }
	}
}
