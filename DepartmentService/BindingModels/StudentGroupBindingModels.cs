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

        public int Capacity { get; set; }

        public int SubgroupsCount { get; set; }

        public long? StewardId { get; set; }

        public long? CuratorId { get; set; }
    }
}
