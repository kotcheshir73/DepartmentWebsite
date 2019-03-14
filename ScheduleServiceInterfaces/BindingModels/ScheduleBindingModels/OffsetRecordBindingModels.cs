namespace ScheduleServiceInterfaces.BindingModels
{
    public class OffsetRecordRecordBindingModel : ScheduleRecordBindingModel
    {
		public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }

        public bool IsStreaming { get; set; }
    }
}