using System;
using System.Drawing;
using System.Text;

namespace DepartmentService.ViewModels
{
	public class LecturerPageViewModel : PageViewModel<LecturerViewModel> { }

	public class LecturerViewModel
	{
		public Guid Id { get; set; }

        public Guid LecturerPostId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Patronymic { get; set; }

		public string Abbreviation { get; set; }

		public DateTime DateBirth { get; set; }

		public string Address { get; set; }

		public string Email { get; set; }

		public string MobileNumber { get; set; }

		public string HomeNumber { get; set; }

		public string Post { get; set; }

        public string LecturerPost { get; set; }

        public double Workload { get; set; }

        public string Rank { get; set; }

        public string Rank2 { get; set; }

        public string Description { get; set; }

		public Image Photo { get; set; }

		public string FullName
		{
			get
			{
				StringBuilder fullname = new StringBuilder(LastName);
				if (FirstName.Length > 0)
				{
					fullname.Append(string.Format(" {0}.", FirstName[0]));
				}
				if (Patronymic.Length > 0)
				{
					fullname.Append(string.Format(" {0}.", Patronymic[0]));
				}
				return fullname.ToString();
			}
		}
	}
}
