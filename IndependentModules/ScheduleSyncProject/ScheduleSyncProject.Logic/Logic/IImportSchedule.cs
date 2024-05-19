using ScheduleSyncProject.Logic.Models.CoreModels;
using ScheduleSyncProject.Logic.Models.ScheduleModels;

namespace ScheduleSyncProject.Logic.Logic;

public interface IImportSchedule
{
	Task<IEnumerable<LessonScheduleModel>?> GetLessonsAsync(DateTime startDate,
		IEnumerable<Classroom> classrooms,
		IEnumerable<Lecturer> lecturers,
		IEnumerable<StudentGroup> studentGroups,
		CancellationToken cancellationToken);

	Task<IEnumerable<string>?> GetGroupsAsync(CancellationToken cancellationToken);

	Task<IEnumerable<LessonScheduleModel>?> GetLessonsAsync(string groupName, 
		DateTime startDate,
		IEnumerable<Classroom> classrooms,
		IEnumerable<Lecturer> lecturers,
		IEnumerable<StudentGroup> studentGroups,
		CancellationToken cancellationToken);
}