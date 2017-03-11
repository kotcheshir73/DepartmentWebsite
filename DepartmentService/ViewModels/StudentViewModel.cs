using System.Drawing;

namespace DepartmentService.ViewModels
{
    public class StudentViewModel
    {
        public long Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Description { get; set; }

        public Image Photo { get; set; }
    }
}
