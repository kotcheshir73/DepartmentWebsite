using Microsoft.EntityFrameworkCore;
using ScheduleSyncProject.Logic.Models.CoreModels;

namespace ScheduleSyncProject.Logic.Logic.Repository;

internal class CoreRepository(ScheduleDbContext context) : ICoreRepository
{
	private readonly ScheduleDbContext _context = context;

	private readonly AutoResetEvent _resetEventSave = new(true);

	private readonly AutoResetEvent _resetEventRemove = new(true);

	public async Task<IEnumerable<SeasonDates>?> GetSeasonDatesAsync(CancellationToken cancellationToken)
	{
		return await Task.FromResult(_context.Database.SqlQuery<SeasonDates>(
			$@"SELECT [Id], [DateBeginFirstHalfSemester] 
				FROM [dbo].[SeasonDates] 
				WHERE [DateDelete] IS NULL")?.ToList());
	}

	public async Task<IEnumerable<Classroom>?> GetClassroomsAsync(CancellationToken cancellationToken)
	{
		return await Task.FromResult(_context.Database.SqlQuery<Classroom>(
			$@"SELECT [Id], [Number] 
				FROM [dbo].[Classrooms] 
				WHERE [DateDelete] IS NULL")?.ToList());
	}

	public async Task<IEnumerable<Lecturer>?> GetLecturersAsync(CancellationToken cancellationToken)
	{
		return await Task.FromResult(_context.Database.SqlQuery<Lecturer>(
			$@"SELECT [Id], [FirstName], [LastName], [Patronymic] 
				FROM [dbo].[Lecturers] 
				WHERE [DateDelete] IS NULL")?.ToList());
	}

	public async Task<IEnumerable<StudentGroup>?> GetStudentGroupsAsync(CancellationToken cancellationToken)
	{
		return await Task.FromResult(_context.Database.SqlQuery<StudentGroup>(
			$@"SELECT [Id], [GroupName] 
				FROM [dbo].[StudentGroups] 
				WHERE [DateDelete] IS NULL")?.ToList());
	}

	public async Task<IEnumerable<SemesterRecord>?> GetSemesterRecordsAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
	{
		return await Task.FromResult(_context.Database.SqlQuery<SemesterRecord>(
			$@"SELECT [Id], 
				[ClassroomId], 
				[StudentGroupId], 
				[LecturerId], 
				[LessonDiscipline], 
				[LessonLecturer], 
				[LessonClassroom], 
				[LessonType], 
				[LessonStudentGroup],
				[ScheduleDate] 
				FROM [dbo].[SemesterRecords] 
				WHERE [ScheduleDate] >= {startDate.ToShortDateString()} AND [ScheduleDate] <= {endDate.ToShortDateString()}")?.ToList());
	}

	public async Task SaveSemesterRecordAsync(SemesterRecord record, CancellationToken cancellationToken)
	{
		_resetEventSave.WaitOne();
		try
		{
			await _context.Database.ExecuteSqlAsync($@"
INSERT INTO [dbo].[SemesterRecords]
	([Id], 
	[ClassroomId], 
	[StudentGroupId], 
	[LecturerId], 
	[LessonDiscipline], 
	[LessonLecturer], 
	[LessonClassroom], 
	[LessonType], 
	[LessonStudentGroup], 
	[ScheduleDate])
VALUES
	({record.Id}, {record.ClassroomId}, {record.StudentGroupId}, 
{record.LecturerId}, '{record.LessonDiscipline}','{record.LessonLecturer}', '{record.LessonClassroom}', 
{(int)record.LessonType}, '{record.LessonStudentGroup}', {record.ScheduleDate})", cancellationToken: cancellationToken);
		}
		finally
		{
			_resetEventSave.Set();
		}
	}

	public async Task RemoveSemesterRecordAsync(Guid id, CancellationToken cancellationToken)
	{
		_resetEventRemove.WaitOne();
		try
		{
			await _context.Database.ExecuteSqlAsync($@"DELETE FROM [dbo].[SemesterRecords]
           WHERE [Id] = {id}", cancellationToken: cancellationToken);
		}
		finally
		{
			_resetEventRemove.Set();
		}
	}

	public ValueTask DisposeAsync() => _context.DisposeAsync();

	public void Dispose() => _context?.Dispose();
}