using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class ScheduleStopWordGetBindingModel
    {
        public long Id { get; set; }
    }
    public class ScheduleStopWordRecordBindingModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string StopWord { get; set; }

        [Required(ErrorMessage = "required")]
        public string StopWordType { get; set; }
    }
}
