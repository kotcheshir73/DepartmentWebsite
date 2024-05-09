using Newtonsoft.Json;

namespace ScheduleSyncProject.Logic.Models.ScheduleModels.TimeTableAPIModels;

public class TimeTableAPIScheduleLesson
{
    [JsonProperty(PropertyName = "group")]
    public required string Group { get; set; }

    [JsonProperty(PropertyName = "nameOfLesson")]
    public required string NameOfLesson { get; set; }

    [JsonProperty(PropertyName = "teacher")]
    public required string Teacher { get; set; }

    [JsonProperty(PropertyName = "room")]
    public required string Room { get; set; }
}