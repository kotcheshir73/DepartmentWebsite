using ScheduleSyncProject.Logic.Models.CoreModels;

namespace ScheduleSyncProject.Logic.Logic.Repository;

public interface ICoreRepository : IAsyncDisposable, IDisposable
{
    Task<IEnumerable<SeasonDates>?> GetSeasonDatesAsync(CancellationToken cancellationToken);

	Task<IEnumerable<Classroom>?> GetClassroomsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<Lecturer>?> GetLecturersAsync(CancellationToken cancellationToken);

    Task<IEnumerable<StudentGroup>?> GetStudentGroupsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<SemesterRecord>?> GetSemesterRecordsAsync(DateTime startDate, DateTime finishDate, CancellationToken cancellationToken);

    Task SaveSemesterRecordAsync(SemesterRecord record, CancellationToken cancellationToken);

    Task RemoveSemesterRecordAsync(Guid id, CancellationToken cancellationToken);
}