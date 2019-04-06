using System;
using System.Drawing;
using System.Text;
using Tools.ViewModels;

namespace Interfaces.ViewModels
{
	public class StudentPageViewModel : PageSettingListViewModel<StudentViewModel> { }

    public class StudentViewModel : PageSettingElementViewModel
    {
        public Guid? StudentGroupId { get; set; }

        public string NumberOfBook { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Patronymic { get; set; }

		public string Email { get; set; }

		public Image Photo { get; set; }

        public bool IsSteward { get; set; }

        public string StudentGroup { get; set; }

        public string Description { get; set; }

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