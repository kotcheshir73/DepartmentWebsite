﻿using AcademicYearInterfaces.HelperModels;
using BaseInterfaces.BindingModels;
using Enums;
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

        public List<BlueAsteriskDisicplineType> DisicplineTypes { get; set; }

        public List<BlueAsteriskBlockType> BlockTypes { get; set; }

        public List<DisciplineBlockSetBindingModel> DisciplineBlocks { get; set; }

        public List<DisciplineSetBindingModel> Disciplines { get; set; }

        public List<TimeNormSetBindingModel> TimeNorms { get; set; }

        public List<BlueAsteriskNewHour> NewHours { get; set; }
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

    public class LecturerDisciplineTimeDistributionsBindingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid UserId { get; set; }
    }

    public class LecturerDisciplineTimeDistributionSetBindingModel
    {
        public Guid DisciplineTimeDistributionId { get; set; }

        public string Comment { get; set; }

        public string CommentWishesOfTeacher { get; set; }

        public List<LecturerDisciplineTimeDistributionElementSetBindingModel> LecturerDisciplineTimeDistributionElements { get; set; }
    }

    public class LecturerDisciplineTimeDistributionElementSetBindingModel
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

    public class ImportDisciplineTimeDistributionsBindingModel
    {
        public Guid AcademicYearId { get; set; }

        public string Path { get; set; }

        public int Semester { get; set; }
    }

    public class UseStreamRecordBindingModel
    {
        public Guid StreamLessonId { get; set; }

        public Guid StreamLessonRecordId { get; set; }
    }
}