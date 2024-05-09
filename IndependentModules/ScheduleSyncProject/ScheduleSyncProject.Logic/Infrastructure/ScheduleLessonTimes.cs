namespace ScheduleSyncProject.Logic.Infrastructure;

internal static class ScheduleLessonTimes
{
	private static readonly List<TimeOnly> _scheduleLessonTimes =
		[
			new TimeOnly(8, 30),
			new TimeOnly(10, 00),
			new TimeOnly(11, 30),
			new TimeOnly(13, 30),
			new TimeOnly(15, 00),
			new TimeOnly(16, 30),
			new TimeOnly(18, 00),
			new TimeOnly(19, 30)
		];

	public static long GetTicks(int number) => 
		(number < 0 || number >= _scheduleLessonTimes.Count) ? 0 : _scheduleLessonTimes[number].Ticks;
}