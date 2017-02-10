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

        public string LessonType { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string ClassroomId { get; set; }

        public long? StudentGroupId { get; set; }

        public long? LecturerId { get; set; }

        /// <summary>
        /// Применять выборку по текстовым данным или по данным из БД (аудитория, дисциплина, преподаватель, группа)
        /// </summary>
        public bool ApplyToAnalogRecordsByTextData { get; set; }

        public bool ApplyToAnalogRecordsByDiscipline { get; set; }

        public bool ApplyToAnalogRecordsByGroup { get; set; }

        public bool ApplyToAnalogRecordsByLecturer { get; set; }

        public bool ApplyToAnalogRecordsByClassroom { get; set; }

        public bool ApplyToAnalogRecordsByLessonType { get; set; }
    }
}
