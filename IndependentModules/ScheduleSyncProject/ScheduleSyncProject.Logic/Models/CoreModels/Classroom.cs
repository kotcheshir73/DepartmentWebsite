namespace ScheduleSyncProject.Logic.Models.CoreModels;

public class Classroom
{
	public Guid Id { get; set; }

	public required string Number { get; set; }

    public override string ToString() => $"{Number}";
}