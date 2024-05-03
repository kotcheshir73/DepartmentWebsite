namespace ScheduleProjectApp.Logic.Models;

public class TimeTableAPIScheduleDay
{
	public int day { get; set; }

	public IList<TimeTableAPIScheduleRecord[]> lessons { get; set; }
}