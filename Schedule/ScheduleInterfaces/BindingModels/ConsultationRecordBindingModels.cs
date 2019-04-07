using System;
using System.ComponentModel.DataAnnotations;

namespace ScheduleInterfaces.BindingModels
{
	public class ConsultationRecordRecordBindingModel : ScheduleSetBindingModel
    {
		public int? Week { get; set; }

		public int? Day { get; set; }

		public int? Lesson { get; set; }

		[Required(ErrorMessage = "required")]
		public DateTime DateConsultation { get; set; }
	}
}