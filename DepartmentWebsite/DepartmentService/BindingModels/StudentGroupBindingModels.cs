using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class StudentGroupGetBindingModel
    {
        public long Id { get; set; }
    }

    public class StudentGroupRecordBindingModel
    {
        public long Id { get; set; }

        public long EducationDirectionId { get; set; }

        [Required(ErrorMessage = "required")]
        public string GroupName { get; set; }
        
        public int Kurs { get; set; }
    }
}
