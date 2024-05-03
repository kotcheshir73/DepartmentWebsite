namespace ScheduleProjectApp.Logic.Models;

internal class ImportToSemesterRecordsBindingModel
{
	public DateTime ScheduleDate { get; set; }

	public List<string> ScheduleUrls { get; set; }

	public string ScheduleAuthUrl { get; set; }

	public string Login { get; set; }

	public string Password { get; set; }
}