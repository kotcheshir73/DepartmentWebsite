using Tools.BindingModels;

namespace ScheduleInterfaces.BindingModels
{
    public class StreamingLessonGetBindingModel : PageSettingGetBinidingModel { }

    public class StreamingLessonSetBindingModel : PageSettingSetBinidingModel
    {
        public string IncomingGroups { get; set; }

        public string StreamName { get; set; }
    }
}