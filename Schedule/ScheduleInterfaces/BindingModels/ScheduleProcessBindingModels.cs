using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ScheduleInterfaces.BindingModels
{
    /// <summary>
    /// Импорт данных с общего сайта
    /// </summary>
    public class ImportToSemesterRecordsBindingModel
    {
        public DateTime ScheduleDate { get; set; }

        public List<string> ScheduleUrls { get; set; }

        public string ScheduleAuthUrl { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }

    public class TimeTableAPIScheduleRecord
    {
        public string group { get; set; }

        public string nameOfLesson { get; set; }

        public string teacher { get; set; }

        public string room { get; set; }
    }

    public class TimeTableAPIScheduleDay
    {
        public int day { get; set; }

        public IList<TimeTableAPIScheduleRecord[]> lessons { get; set; }
    }

    public class TimeTableAPIScheduleWeekNumber
    {
        public TimeTableAPIScheduleDay[] days { get; set; }
    }

    public class TimeTableAPIScheduleWeek
    {
        [JsonProperty(PropertyName = "0")]
        public TimeTableAPIScheduleWeekNumber _0 { get; set; }
        [JsonProperty(PropertyName = "1")]
        public TimeTableAPIScheduleWeekNumber _1 { get; set; }
        [JsonProperty(PropertyName = "2")]
        public TimeTableAPIScheduleWeekNumber _2 { get; set; }
        [JsonProperty(PropertyName = "3")]
        public TimeTableAPIScheduleWeekNumber _3 { get; set; }
        [JsonProperty(PropertyName = "4")]
        public TimeTableAPIScheduleWeekNumber _4 { get; set; }
        [JsonProperty(PropertyName = "5")]
        public TimeTableAPIScheduleWeekNumber _5 { get; set; }
        [JsonProperty(PropertyName = "6")]
        public TimeTableAPIScheduleWeekNumber _6 { get; set; }
        [JsonProperty(PropertyName = "7")]
        public TimeTableAPIScheduleWeekNumber _7 { get; set; }
        [JsonProperty(PropertyName = "8")]
        public TimeTableAPIScheduleWeekNumber _8 { get; set; }
        [JsonProperty(PropertyName = "9")]
        public TimeTableAPIScheduleWeekNumber _9 { get; set; }
        [JsonProperty(PropertyName = "10")]
        public TimeTableAPIScheduleWeekNumber _10 { get; set; }
        [JsonProperty(PropertyName = "11")]
        public TimeTableAPIScheduleWeekNumber _11 { get; set; }
        [JsonProperty(PropertyName = "12")]
        public TimeTableAPIScheduleWeekNumber _12 { get; set; }
        [JsonProperty(PropertyName = "13")]
        public TimeTableAPIScheduleWeekNumber _13 { get; set; }
        [JsonProperty(PropertyName = "14")]
        public TimeTableAPIScheduleWeekNumber _14 { get; set; }
        [JsonProperty(PropertyName = "15")]
        public TimeTableAPIScheduleWeekNumber _15 { get; set; }
    }

    public class TimeTableAPIScheduleResponse
    {
        public TimeTableAPIScheduleWeek weeks { get; set; }
    }

    public class TimeTableAPIScheduleAnswer
    {
        public TimeTableAPIScheduleResponse response { get; set; }

        public string error { get; set; }
    }
    public class TimeTableAPIScheduleAllGroupsAnswer
    {
        public string[] response { get; set; }

        public string error { get; set; }
    }

    /// <summary>
    /// Загружаем данные по зачетам
    /// </summary>
    public class ImportToOffsetFromExcel
    {
        public DateTime ScheduleDate { get; set; }

        public string FileName { get; set; }
    }

    /// <summary>
    /// Загружаем данные по экзаменам
    /// </summary>
    public class ImportToExaminationFromExcel
    {
        public DateTime ScheduleDate { get; set; }

        public string FileName { get; set; }
    }

    public class LoadScheduleBindingModel
    {
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ClassroomNumber { get; set; }

        public Guid? ClassroomId { get; set; }

        public string StudentGroupName { get; set; }

        public Guid? StudentGroupId { get; set; }

        public string DisciplineName { get; set; }

        public Guid? DisciplineId { get; set; }

        public string LecturerName { get; set; }

        public Guid? LecturerId { get; set; }
    }
}
