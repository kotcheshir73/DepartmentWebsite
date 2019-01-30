using System;
using System.ComponentModel.DataAnnotations;

namespace ScheduleServiceInterfaces.BindingModels
{
	public class ConsultationRecordRecordBindingModel : ScheduleRecordBindingModel
    {
		public int? Week { get; set; }

		public int? Day { get; set; }

		public int? Lesson { get; set; }

		[Required(ErrorMessage = "required")]
		public DateTime DateConsultation { get; set; }
	}
}