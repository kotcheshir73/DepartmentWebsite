namespace ScheduleProject.Logic.Models.CoreModels;

public class Classroom : IdEntity
{
    public required string Number { get; set; }

    public override string ToString() => $"{Number}";
}
