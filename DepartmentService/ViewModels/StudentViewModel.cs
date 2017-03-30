using System.Collections.Generic;
using System.Drawing;

namespace DepartmentService.ViewModels
{
	public class StudentPageViewModel
	{
		public int MaxCount { get; set; }

		public List<StudentViewModel> List { get; set; }
	}

    public class StudentViewModel
    {
        public string NumberOfBook { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        public Image Photo { get; set; }

        public long? StudentGroupId { get; set; }

        public string StudentGroup { get; set; }

        public string Description { get; set; }
    }

    public class StudentHistoryViewModel
    {
        public long Id { get; set; }

        public string NumberOfBook { get; set; }

        public string DateCreate { get; set; }

        public string TextMessage { get; set; }
    }
}
