using ScheduleSyncProject.Logic.Models.CoreModels;

namespace ScheduleSyncProject.Logic.Logic.Repository;

public interface ICoreRepository
{
    Task<IEnumerable<Classroom>?> GetClassroomsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<Lecturer>?> GetLecturersAsync(CancellationToken cancellationToken);

    Task<IEnumerable<StudentGroup>?> GetStudentGroupsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<SemesterRecord>?> GetSemesterRecordsAsync(DateTime startDate, DateTime finishDate, CancellationToken cancellationToken);

    Task SaveSemesterRecordAsync(SemesterRecord record, CancellationToken cancellationToken);

    Task RemoveSemesterRecordsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
}