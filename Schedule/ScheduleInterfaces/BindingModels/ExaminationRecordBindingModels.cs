using System;
using System.ComponentModel.DataAnnotations;

namespace ScheduleInterfaces.BindingModels
{
	public class ExaminationRecordSetBindingModel : ScheduleSetBindingModel
    {
		[Required(ErrorMessage = "required")]
		public DateTime DateConsultation { get; set; }
        
        public string LessonConsultationClassroom { get; set; }

        public Guid? ConsultationClassroomId { get; set; }
    }
}