using Enums;
using System;
using Tools.ViewModels;

namespace LearningProgressInterfaces.ViewModels
{
    public class DisciplineLessonPageViewModel : PageSettingListViewModel<DisciplineLessonViewModel> { }

    public class DisciplineLessonViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid EducationDirectionId { get; set; }

        public Guid TimeNormId { get; set; }

        public string AcademicYear { get; set; }

        public string Discipline { get; set; }

        public string EducationDirection { get; set; }

        public string TimeNorm { get; set; }

        public Semesters Semester { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

        public int CountOfPairs { get; set; }

        public int CountTasks { get; set; }

        public DateTime? Date { get; set; }

        public byte[] DisciplineLessonFile { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}