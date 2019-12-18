using System.ComponentModel.DataAnnotations;

namespace ScheduleInterfaces.BindingModels
{
    public class ConsultationRecordSetBindingModel : ScheduleSetBindingModel
    {
        [Required(ErrorMessage = "required")]
        public int ConsultationTime { get; set; }
    }
}