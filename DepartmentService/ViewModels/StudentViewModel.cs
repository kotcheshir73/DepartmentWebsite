using System.Drawing;

namespace DepartmentService.ViewModels
{
    public class StudentViewModel
    {
        public string NumberOfBook { get; set; }
        
        public string LastName { get; set; }
        
        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        public Image Photo { get; set; }

        public long StudentGroupId { get; set; }

        public string StudentGroup { get; set; }

        public string Description { get; set; }
    }
}
