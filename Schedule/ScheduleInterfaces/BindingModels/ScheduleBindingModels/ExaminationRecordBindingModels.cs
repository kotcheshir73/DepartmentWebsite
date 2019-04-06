using System;
using System.ComponentModel.DataAnnotations;

namespace ScheduleServiceInterfaces.BindingModels
{
	public class ExaminationRecordRecordBindingModel : ScheduleRecordBindingModel
    {
		[Required(ErrorMessage = "required")]
		public DateTime DateConsultation { get; set; }

		[Required(ErrorMessage = "required")]
		public DateTime DateExamination { get; set; }
        
        public string LessonConsultationClassroom { get; set; }

        public Guid? ConsultationClassroomId { get; set; }
    }
}