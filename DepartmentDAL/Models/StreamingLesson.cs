namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, хранящий в себе сведенья о потоковых занятиях
    /// </summary>
    public class StreamingLesson : BaseEntity
    {
        public string IncomingGroups { get; set; }

        public string StreamName { get; set; }
    }
}
