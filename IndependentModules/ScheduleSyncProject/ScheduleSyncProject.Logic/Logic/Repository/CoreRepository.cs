using Microsoft.EntityFrameworkCore;
using ScheduleSyncProject.Logic.Models.CoreModels;

namespace ScheduleSyncProject.Logic.Logic.Repository;

internal class CoreRepository(ScheduleDbContext context) : ICoreRepository
{
    private readonly ScheduleDbContext _context = context;

    public async Task<IEnumerable<Classroom>?> GetClassroomsAsync(CancellationToken cancellationToken)
    {
        return await Task.FromResult(_context.Database.SqlQuery<Classroom>(
            $@"SELECT [Id], [Number] 
				FROM [dbo].[Classrooms] 
				WHERE [DateDelete] IS NULL"));
    }

    public async Task<IEnumerable<Lecturer>?> GetLecturersAsync(CancellationToken cancellationToken)
    {
        return await Task.FromResult(_context.Database.SqlQuery<Lecturer>(
            $@"SELECT [Id], [FirstName], [LastName], [Patronymic] 
				FROM [dbo].[Lecturers] 
				WHERE [DateDelete] IS NULL"));
    }

    public async Task<IEnumerable<StudentGroup>?> GetStudentGroupsAsync(CancellationToken cancellationToken)
    {
        return await Task.FromResult(_context.Database.SqlQuery<StudentGroup>(
            $@"SELECT [Id], [GroupName] 
				FROM [dbo].[StudentGroups] 
				WHERE [DateDelete] IS NULL"));
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
				WHERE [ScheduleDate] >= {startDate.ToShortDateString()} AND [ScheduleDate] <= {endDate.ToShortDateString()}"));
    }

    public async Task SaveSemesterRecordAsync(SemesterRecord record, CancellationToken cancellationToken)
    {
        await _context.Database.ExecuteSqlAsync($@"INSERT INTO [dbo].[SemesterRecords]
           ([Id]
           ,[ClassroomId]
           ,[StudentGroupId]
           ,[LecturerId]
           ,[LessonDiscipline]
           ,[LessonLecturer]
           ,[LessonClassroom]
           ,[LessonType]
           ,[LessonStudentGroup]
           ,[ScheduleDate])
     VALUES
           ({record.Id}
           ,{record.ClassroomId}
           ,{record.StudentGroupId}
           ,{record.LecturerId}
           ,{record.LessonDiscipline}
           ,{record.LessonLecturer}
           ,{record.LessonClassroom}
           ,{record.LessonType}
           ,{record.LessonStudentGroup}
           ,{record.ScheduleDate})", cancellationToken: cancellationToken);
    }

    public async Task RemoveSemesterRecordsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        if (ids == null || !ids.Any())
        {
            return;
        }

        await _context.Database.ExecuteSqlAsync($@"DELETE FROM [dbo].[SemesterRecords]
           WHERE [Id] in ({string.Join(",", ids)})", cancellationToken: cancellationToken);
    }
}