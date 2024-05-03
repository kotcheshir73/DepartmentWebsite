using Newtonsoft.Json;

namespace ScheduleProject.Logic.Models.ScheduleModels.TimeTableAPI;

public class TimeTableAPIScheduleDay
{
    [JsonProperty(PropertyName = "day")]
    public int Day { get; set; }

    [JsonProperty(PropertyName = "lessons")]
    public required IList<TimeTableAPIScheduleLesson[]> Lessons { get; set; }
}