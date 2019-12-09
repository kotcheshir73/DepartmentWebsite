namespace ScheduleInterfaces.BindingModels
{
    public class OffsetRecordSetBindingModel : ScheduleSetBindingModel
    {
		public int Week { get; set; }

		public int Day { get; set; }

		public int Lesson { get; set; }
    }
}