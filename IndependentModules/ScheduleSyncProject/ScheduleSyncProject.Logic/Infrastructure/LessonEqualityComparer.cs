using ScheduleSyncProject.Logic.Models.ScheduleModels;
using System.Diagnostics.CodeAnalysis;

namespace ScheduleSyncProject.Logic.Infrastructure;

internal class LessonEqualityComparer : IEqualityComparer<LessonScheduleModel>
{
	public bool Equals(LessonScheduleModel? x, LessonScheduleModel? y)
	{
		if (x == null || y == null)
		{
			return false;
		}

        return x.Classroom == y.Classroom && x.Lecturer == y.Lecturer &&
			x.Group == y.Group && x.Discipline == y.Discipline && x.LessonType == y.LessonType;
	}

	public int GetHashCode([DisallowNull] LessonScheduleModel obj)
	{
		return obj.GetHashCode();
	}
}
