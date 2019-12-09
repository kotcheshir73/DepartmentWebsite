using System;
using System.ComponentModel.DataAnnotations;

namespace ScheduleInterfaces.BindingModels
{
	public class ConsultationRecordSetBindingModel : ScheduleSetBindingModel
    {
		[Required(ErrorMessage = "required")]
		public DateTime DateConsultation { get; set; }
	}
}