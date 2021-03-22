using Enums;
using System;
using System.ComponentModel;
using Tools.ViewModels;

namespace WebInterfaces.ViewModels
{
    public class WebAcademicYearPageViewModel : PageSettingListViewModel<WebAcademicYearViewModel> { }

    public class WebAcademicYearViewModel : PageSettingElementViewModel
    {
        public string Title { get; set; }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public class WebAcademicPlanPageViewModel : PageSettingListViewModel<WebAcademicPlanViewModel> { }

    public class WebAcademicPlanViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public string AcademicYear { get; set; }

        public Guid? EducationDirectionId { get; set; }

        [DisplayName("Направление")]
        public string EducationDirection { get; set; }

        [DisplayName("Курсы")]
        public string AcademicCoursesStrings { get; set; }

        public int? AcademicCourses { get; set; }

        public override string ToString()
        {
            return string.Format("{0}. {1} курсы", EducationDirection, AcademicCoursesStrings);
        }

        public AcademicCourse academicCourse()
        {
            return (AcademicCourse)Enum.ToObject(typeof(AcademicCourse), AcademicCourses);
        }
    }

    public class WebAcademicPlanRecordPageViewModel : PageSettingListViewModel<WebAcademicPlanRecordViewModel> { }

    public class WebAcademicPlanRecordViewModel : PageSettingElementViewModel
    {
        public Guid AcademicPlanId { get; set; }

        public Guid DisciplineId { get; set; }

        [DisplayName("Дисциплина")]
        public string Disciplne { get; set; }

        [DisplayName("Семестр")]
        public string Semester { get; set; }

        public Guid? ContingentId { get; set; }

        [DisplayName("Контингент")]
        public string ContingentGroup { get; set; }

        [DisplayName("Зет")]
        public int Zet { get; set; }

        public Guid? AcademicPlanRecordParentId { get; set; }

        [DisplayName("Родительская")]
        public bool IsParent { get; set; }

        [DisplayName("Дисц. по выб.")]
        public bool Selectable { get; set; }

        [DisplayName("Участвует в расчете")]
        public bool IsSelected { get; set; }
    }

    public class WebAcademicPlanRecordElementPageViewModel : PageSettingListViewModel<WebAcademicPlanRecordElementViewModel> { }

    public class WebAcademicPlanRecordElementViewModel : PageSettingElementViewModel
    {
        public Guid AcademicPlanRecordId { get; set; }

        public Guid TimeNormId { get; set; }

        [DisplayName("Дисциплина")]
        public string Disciplne { get; set; }

        [DisplayName("Вид нагрузки")]
        public string KindOfLoadName { get; set; }

        [DisplayName("План. часы")]
        public decimal PlanHours { get; set; }

        [DisplayName("Факт. часы")]
        public decimal FactHours { get; set; }
    }

    public class WebAcademicPlanRecordMissionPageViewModel : PageSettingListViewModel<WebAcademicPlanRecordMissionViewModel> { }

    public class WebAcademicPlanRecordMissionViewModel : PageSettingElementViewModel
    {
        public Guid AcademicPlanRecordElementId { get; set; }

        public Guid LecturerId { get; set; }

        [DisplayName("Нагрузка")]
        public string AcademicPlanRecordElementTitle { get; set; }

        [DisplayName("Преподаватель")]
        public string LecturerName { get; set; }

        [DisplayName("Часы")]
        public decimal Hours { get; set; }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public class WebTimeNormPageViewModel : PageSettingListViewModel<WebTimeNormViewModel> { }

    public class WebTimeNormViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public string AcademicYear { get; set; }

        public Guid DisciplineBlockId { get; set; }

        public string DisciplineBlockName { get; set; }

        [DisplayName("Название")]
        public string TimeNormName { get; set; }

        [DisplayName("Краткое")]
        public string TimeNormShortName { get; set; }

        public int TimeNormOrder { get; set; }

        public string TimeNormEducationDirectionQualification { get; set; }

        [DisplayName("Вид нагрузки")]
        public string KindOfLoadName { get; set; }

        public string KindOfLoadAttributeName { get; set; }

        public string KindOfLoadBlueAsteriskName { get; set; }

        public string KindOfLoadBlueAsteriskAttributeName { get; set; }

        public string KindOfLoadBlueAsteriskPracticName { get; set; }

        [DisplayName("Тип нагрузки")]
        public string KindOfLoadType { get; set; }

        [DisplayName("Часы")]
        public decimal? Hours { get; set; }

        [DisplayName("Числ. коэф.")]
        public decimal? NumKoef { get; set; }

        [DisplayName("Коэф. норм. вр.")]
        public string TimeNormKoef { get; set; }

        public bool UseInLearningProgress { get; set; }

        public bool UseInSite { get; set; }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public class WebStreamLessonPageViewModel : PageSettingListViewModel<WebStreamLessonViewModel> { }

    public class WebStreamLessonViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public string AcademicYear { get; set; }

        [DisplayName("Название потока")]
        public string StreamLessonName { get; set; }

        [DisplayName("Часы")]
        public decimal StreamLessonHours { get; set; }

        [DisplayName("Семестр")]
        public string Semester { get; set; }

        public WebStreamLessonRecordPageViewModel Records { get; set; }
    }

    public class WebStreamLessonRecordPageViewModel : PageSettingListViewModel<WebStreamLessonRecordViewModel> { }

    public class WebStreamLessonRecordViewModel : PageSettingElementViewModel
    {
        public Guid StreamLessonId { get; set; }

        public Guid AcademicPlanRecordElementId { get; set; }

        public Guid AcademicPlanRecordId { get; set; }

        public Guid AcademicPlanId { get; set; }

        public string StreamLessonName { get; set; }

        [DisplayName("Запись")]
        public string AcademicPlanRecordElementText { get; set; }

        [DisplayName("Считать по этой записи часы")]
        public bool IsMain { get; set; }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public class WebContingentPageViewModel : PageSettingListViewModel<WebContingentViewModel> { }

    public class WebContingentViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public string AcademicYear { get; set; }

        public Guid EducationDirectionId { get; set; }

        [DisplayName("Направление")]
        public string EducationDirection { get; set; }

        [DisplayName("Наименование")]
        public string ContingentName { get; set; }

        [DisplayName("Курс")]
        public string Course { get; set; }

        [DisplayName("Количество групп")]
        public int CountGroups { get; set; }

        [DisplayName("Количество студентов")]
        public int CountStudents { get; set; }

        [DisplayName("Количество подгрупп")]
        public int CountSubgroups { get; set; }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public class WebLecturerWorkloadPageViewModel : PageSettingListViewModel<WebLecturerWorkloadViewModel> { }

    public class WebLecturerWorkloadViewModel : PageSettingElementViewModel
    {
        public Guid AcademicYearId { get; set; }

        public string AcademicYear { get; set; }

        public Guid LecturerId { get; set; }

        [DisplayName("Преподаватель")]
        public string Lecturer { get; set; }

        [DisplayName("Нагрузка")]
        public double Workload { get; set; }
    }
}
