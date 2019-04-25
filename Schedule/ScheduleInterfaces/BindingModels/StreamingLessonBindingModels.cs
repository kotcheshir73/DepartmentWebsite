using Tools.BindingModels;

namespace ScheduleInterfaces.BindingModels
{
    public class StreamingLessonGetBindingModel : PageSettingGetBinidingModel
    {
        public string IncomingGroups { get; set; }
    }

    public class StreamingLessonSetBindingModel : PageSettingSetBinidingModel
    {
        public string IncomingGroups { get; set; }

        public string StreamName { get; set; }
    }
}