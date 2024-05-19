namespace ScheduleSyncProject.Logic.Models.CoreModels;

public class StudentGroup
{
	public Guid Id { get; set; }

	public required string GroupName { get; set; }

	public override string ToString() => $"{GroupName}";
}