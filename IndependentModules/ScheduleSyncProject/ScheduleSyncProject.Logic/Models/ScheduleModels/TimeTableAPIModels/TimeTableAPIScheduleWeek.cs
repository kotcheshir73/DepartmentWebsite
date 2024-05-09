using Newtonsoft.Json;

namespace ScheduleSyncProject.Logic.Models.ScheduleModels.TimeTableAPIModels;

public class TimeTableAPIScheduleWeek
{
    [JsonProperty(PropertyName = "days")]
    public required TimeTableAPIScheduleDay[] Days { get; set; }
}