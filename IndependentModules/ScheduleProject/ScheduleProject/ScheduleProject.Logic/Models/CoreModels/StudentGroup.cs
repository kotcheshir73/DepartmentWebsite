namespace ScheduleProject.Logic.Models.CoreModels;

public class StudentGroup : IdEntity
{
	public required string GroupName { get; set; }

	public override string ToString() => $"{GroupName}";
}