using System;
using Tools.BindingModels;

namespace WebInterfaces.BindingModels
{
    public class WebAcademicYearGetBindingModel : PageSettingGetBinidingModel { }

    public class WebAcademicYearSetBindingModel : PageSettingSetBinidingModel
    {
        public string Title { get; set; }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public class WebAcademicPlanGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicYearId { get; set; }
    }

    public class WebAcademicPlanSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid? EducationDirectionId { get; set; }

        public int? AcademicCourses { get; set; }
    }

    public class WebAcademicPlanRecordGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicPlanId { get; set; }
    }

    public class WebAcademicPlanRecordSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicPlanId { get; set; }

        public Guid DisciplineId { get; set; }

        public Guid? ContingentId { get; set; }

        public string Semester { get; set; }

        public int Zet { get; set; }

        public Guid? AcademicPlanRecordParentId { get; set; }

        public bool IsParent { get; set; }

        public bool Selectable { get; set; }

        public bool IsSelected { get; set; }
    }

    public class WebAcademicPlanRecordElementGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicPlanRecordId { get; set; }

        public Guid? TimeNormId { get; set; }
    }

    public class WebAcademicPlanRecordElementSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicPlanRecordId { get; set; }

        public Guid TimeNormId { get; set; }

        public decimal PlanHours { get; set; }

        public decimal FactHours { get; set; }
    }

    public class WebAcademicPlanRecordMissionGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicPlanRecordElementId { get; set; }

        public Guid? LecturerId { get; set; }

        public Guid? AcademicYearId { get; set; }
    }

    public class WebAcademicPlanRecordMissionSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicPlanRecordElementId { get; set; }

        public Guid LecturerId { get; set; }

        public decimal Hours { get; set; }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public class WebStreamLessonGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicYearId { get; set; }
    }

    public class WebStreamLessonSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicYearId { get; set; }

        public decimal StreamLessonHours { get; set; }

        public string StreamLessonName { get; set; }

        public string Semester { get; set; }
    }

    public class WebStreamLessonRecordGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? StreamLessonId { get; set; }
    }

    public class WebStreamLessonRecordSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid StreamLessonId { get; set; }

        public Guid AcademicPlanRecordElementId { get; set; }

        public bool IsMain { get; set; }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public class WebTimeNormGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicYearId { get; set; }

        public Guid? AcademicPlanRecordId { get; set; }

        public Guid? DisciplineBlockId { get; set; }
    }

    public class WebTimeNormSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineBlockId { get; set; }

        public string TimeNormName { get; set; }

        public string TimeNormShortName { get; set; }

        public int TimeNormOrder { get; set; }

        public string TimeNormEducationDirectionQualification { get; set; }

        public string KindOfLoadName { get; set; }

        public string KindOfLoadAttributeName { get; set; }

        public string KindOfLoadBlueAsteriskName { get; set; }

        public string KindOfLoadBlueAsteriskAttributeName { get; set; }

        public string KindOfLoadBlueAsteriskPracticName { get; set; }

        public string KindOfLoadType { get; set; }

        public decimal? Hours { get; set; }

        public decimal? NumKoef { get; set; }

        public string TimeNormKoef { get; set; }

        public bool UseInLearningProgress { get; set; }

        public bool UseInSite { get; set; }

        /// <summary>
        /// Код вида работ в справочнике видов работ в новой версии планов, чтобы потом искать работу в строках плана
        /// </summary>
        public string KindOfLoadBlueAsteriskCode { get; set; }

        /// <summary>
        /// Код вида практики в справочнике видов практик в новой версии планов, чтобы потом искать практику в строках плана
        /// </summary>
        public string KindOfLoadBlueAsteriskPracticCode { get; set; }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public class WebContingentGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicYearId { get; set; }

        public Guid? AcademicPlanId { get; set; }

        public Guid? EducationDirectionId { get; set; }
    }

    public class WebContingentSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid EducationDirectionId { get; set; }

        public string ContingentName { get; set; }

        public int Course { get; set; }

        public int CountGroups { get; set; }

        public int CountStudents { get; set; }

        public int CountSubgroups { get; set; }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------

    public class WebLecturerWorkloadGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? AcademicYearId { get; set; }

        public Guid? LecturerId { get; set; }
    }

    public class WebLecturerWorkloadSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid LecturerId { get; set; }

        public double Workload { get; set; }
    }
}
