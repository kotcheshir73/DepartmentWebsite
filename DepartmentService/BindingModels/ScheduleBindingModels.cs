using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class ScheduleBindingModel
    {
        public string ClassroomId { get; set; }

        public DateTime? DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }
    }

    public class LoadHTMLForClassroomsBindingModel
    {
        public string ScheduleUrl { get; set; }

        public List<string> Classrooms { get; set; }
    }

    public class ExportToExcelClassroomsBindingModel
    {
        public string FileName { get; set; }

        public List<string> Classrooms { get; set; }

        public long SeasonDatesId { get; set; }
    }

    public class ExportToHTMLClassroomsBindingModel
    {
        public string FilePath { get; set; }

        public List<string> Classrooms { get; set; }

        public long SeasonDatesId { get; set; }
    }

    public class ImportToOffsetFromExcel
    {
        public string FileName { get; set; }
    }

    public class ImportToExaminationFromExcel
    {
        public string FileName { get; set; }
    }

    public class ScheduleStopWordGetBindingModel
    {
        public long Id { get; set; }
    }

    public class ScheduleStopWordRecordBindingModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string StopWord { get; set; }

        [Required(ErrorMessage = "required")]
        public string StopWordType { get; set; }
    }

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

        public bool IsStreaming { get; set; }

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

    public class ConsultationRecordGetBindingModel
    {
        public long Id { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }
    }

    public class ConsultationRecordRecordBindingModel
    {
        public long Id { get; set; }

        public int? Week { get; set; }

        public int? Day { get; set; }

        public int? Lesson { get; set; }

        public DateTime DateConsultation { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string ClassroomId { get; set; }

        public long? StudentGroupId { get; set; }

        public long? LecturerId { get; set; }
    }

    public class OffsetRecordGetBindingModel
    {
        public long Id { get; set; }
    }

    public class OffsetRecordRecordBindingModel
    {
        public long Id { get; set; }

        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public string ClassroomId { get; set; }

        public long? StudentGroupId { get; set; }

        public long? LecturerId { get; set; }
    }

    public class ExaminationRecordGetBindingModel
    {
        public long Id { get; set; }
    }

    public class ExaminationRecordRecordBindingModel
    {
        public long Id { get; set; }

        public DateTime DateConsultation { get; set; }

        public DateTime DateExamination { get; set; }

        public string LessonDiscipline { get; set; }

        public string LessonLecturer { get; set; }

        public string LessonGroup { get; set; }

        public string LessonClassroom { get; set; }

        public long? LecturerId { get; set; }

        public long? StudentGroupId { get; set; }

        public string ClassroomId { get; set; }
    }
}
