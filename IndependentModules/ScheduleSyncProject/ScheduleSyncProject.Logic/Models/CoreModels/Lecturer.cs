namespace ScheduleSyncProject.Logic.Models.CoreModels;

public class Lecturer
{
	public Guid Id { get; set; }

	public required string FirstName { get; set; }

	public required string LastName { get; set; }

	public string? Patronymic { get; set; }

	public override string ToString() => 
		$"{LastName} {FirstName[0]}.{(string.IsNullOrEmpty(Patronymic) ? "" : $" {Patronymic[0]}.")}";
}