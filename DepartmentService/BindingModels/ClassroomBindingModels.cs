using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class ClassroomGetBindingModel
    {
        public string Id { get; set; }
    }

    public class ClassroomRecordBindingModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string ClassroomType { get; set; }

        [Required(ErrorMessage = "required")]
        public int Capacity { get; set; }
    }
}
