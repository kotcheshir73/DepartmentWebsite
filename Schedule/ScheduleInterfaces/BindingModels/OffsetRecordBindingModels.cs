namespace ScheduleInterfaces.BindingModels
{
    public class OffsetRecordRecordBindingModel : ScheduleSetBindingModel
    {
		public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }

        public bool IsStreaming { get; set; }
    }
}