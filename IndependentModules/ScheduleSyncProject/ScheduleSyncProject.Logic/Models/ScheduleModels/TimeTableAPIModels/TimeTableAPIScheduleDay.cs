using Newtonsoft.Json;

namespace ScheduleSyncProject.Logic.Models.ScheduleModels.TimeTableAPIModels;

public class TimeTableAPIScheduleDay
{
    [JsonProperty(PropertyName = "day")]
    public int Day { get; set; }

    [JsonProperty(PropertyName = "lessons")]
    public required IList<TimeTableAPIScheduleLesson[]> Lessons { get; set; }
}