using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class StudentGetBindingModel
    {
        public string NumberOfBook { get; set; }

        public long? StudentGroupId { get; set; }
    }

    public class StudentRecordBindingModel
    {
        [Required(ErrorMessage = "required")]
        public string NumberOfBook { get; set; }

        [Required(ErrorMessage = "required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "required")]
        public string LastName { get; set; }
        
        public string Patronymic { get; set; }

        public string Description { get; set; }

        public long StudentGroupId { get; set; }

        public byte[] Photo { get; set; }
    }
}
