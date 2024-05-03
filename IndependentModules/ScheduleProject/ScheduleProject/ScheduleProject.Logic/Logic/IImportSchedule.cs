using ScheduleProject.Logic.Models.CoreModels;
using ScheduleProject.Logic.Models.ScheduleModels;

namespace ScheduleProject.Logic.Logic;

public interface IImportSchedule
{
	Task<IEnumerable<LessonScheduleModel>?> GetLessonsAsync(string baseUrl,
		DateTime startDate,
		IEnumerable<Classroom> classrooms,
		IEnumerable<Lecturer> lecturers,
		IEnumerable<StudentGroup> studentGroups,
		CancellationToken cancellationToken);
}