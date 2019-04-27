using Enums;
using Models.AcademicYearData;
using Models.Base;
using Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Xml;
using Tools;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class EducationalProcessLoadFromXMLBindingModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }
    }

    public class EducationalProcessDuplicateAcademicYear
    {
        public Guid FromAcademicPlanId { get; set; }

        public Guid ToAcademicPlanId { get; set; }

        public bool DuplicateAcademicPlan { get; set; }

        public bool DuplicateTimeNorm { get; set; }

        public bool DuplicateContingent { get; set; }

        public bool DuplicateSeasonDate { get; set; }
    }

    public class EducationalProcessCreateStreams
    {
        public Guid AcademicYearId { get; set; }
    }

    public class ParseDisciplineBindingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid AcademicPlanId { get; set; }

        public Guid DisciplineBlockId { get; set; }

        public int Counter { get; set; }

        public XmlNode Node { get; set; }

        public string KafedraNumber { get; set; }

        public ResultService Result { get; set; }

        public List<Semesters> Semesters { get; set; }
    }

    public class ParseFinalBindingModel
    {
        public Guid AcademicPlanId { get; set; }

        public string AcademicLevel { get; set; }

        public Guid DisciplineBlockId { get; set; }

        public ResultService Result { get; set; }

        public XmlNode Node { get; set; }

        public int SemesterNumber { get; set; }
    }

    public class ParseBlueAsterisk
    {
        public Guid AcademicYearId { get; set; }

        public Guid AcademicPlanId { get; set; }

        public XmlNode Node { get; set; }

        public List<Semesters> Semesters { get; set; }

        public List<BlueAsteriskTypeObject> ObjectTypes { get; set; }

        public List<BlueAsteriskBlockType> BlockTypes { get; set; }

        public List<DisciplineBlock> DisciplineBlocks { get; set; }

        public List<Discipline> Disciplines { get; set; }

        public List<TimeNorm> TimeNorms { get; set; }
    }

    public class AcademicPlanRecordsForDiciplineBindingModel : PageSettingGetBinidingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineId { get; set; }
    }

    public class ImportLecturerWorkloadBindingModel
    {
        public Guid AcademicYearId { get; set; }

        public string Path { get; set; }
    }

    public class LecturerDisciplineTimeDistributions
    {
        public Guid AcademicYearId { get; set; }

        public Guid UserId { get; set; }
    }

    public class LecturerDisciplineTimeDistributionSet
    {
        public Guid DisciplineTimeDistributionId { get; set; }

        public string Comment { get; set; }

        public string CommentWishesOfTeacher { get; set; }

        public List<LecturerDisciplineTimeDistributionElementSet> LecturerDisciplineTimeDistributionElements { get; set; }
    }

    public class LecturerDisciplineTimeDistributionElementSet
    {
        public Guid? DisciplineTimeDistributionRecordFirstWeekFirstHalfId { get; set; }

        public Guid? DisciplineTimeDistributionRecordSecondWeekFirstHalfId { get; set; }

        public Guid? DisciplineTimeDistributionRecordFirstWeekSecondHalfId { get; set; }

        public Guid? DisciplineTimeDistributionRecordSecondWeekSecondHalfId { get; set; }

        public Guid? DisciplineTimeDistributionClassroomId { get; set; }

        public double? DisciplineTimeDistributionRecordFirstWeekFirstHalf { get; set; }

        public double? DisciplineTimeDistributionRecordSecondWeekFirstHalf { get; set; }

        public double? DisciplineTimeDistributionRecordFirstWeekSecondHalf { get; set; }

        public double? DisciplineTimeDistributionRecordSecondWeekSecondHalf { get; set; }

        public string DisciplineTimeDistributionClassroom { get; set; }
    }
}