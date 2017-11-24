namespace DepartmentService.BindingModels
{
    public class StreamingLessonGetBindingModel : PageSettingBinidingModel
	{
        public long? Id { get; set; }
	}

    public class StreamingLessonRecordBindingModel
    {
        public long Id { get; set; }

        public string IncomingGroups { get; set; }

        public string StreamName { get; set; }
    }
}
