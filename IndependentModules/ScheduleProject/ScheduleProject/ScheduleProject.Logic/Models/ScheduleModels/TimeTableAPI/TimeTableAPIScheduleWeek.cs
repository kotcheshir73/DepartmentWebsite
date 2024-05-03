using Newtonsoft.Json;

namespace ScheduleProject.Logic.Models.ScheduleModels.TimeTableAPI;

public class TimeTableAPIScheduleWeek
{
    [JsonProperty(PropertyName = "days")]
    public required TimeTableAPIScheduleDay[] Days { get; set; }
}