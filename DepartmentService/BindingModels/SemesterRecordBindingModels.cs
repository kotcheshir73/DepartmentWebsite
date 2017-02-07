using DepartmentDAL.Enums;

namespace DepartmentService.BindingModels
{
    public class SemesterRecordGetBindingModel
    {
        public long Id { get; set; }
    }
    public class SemesterRecordRecordBindingModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public LessonTypes LessonType { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonTeacher { get; set; }

        public string LessonGroupName { get; set; }

        public long? StudentGroupId { get; set; }

        public string ClassroomId { get; set; }

        public bool ApplyToAnalogRecords { get; set; }
    }
}
