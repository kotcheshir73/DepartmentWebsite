namespace ScheduleSyncProject.Logic.Models.CoreModels;

public class StudentGroup
{
	public Guid Id { get; set; } = Guid.NewGuid();

	public required string GroupName { get; set; }

	public override string ToString() => $"{GroupName}";
}