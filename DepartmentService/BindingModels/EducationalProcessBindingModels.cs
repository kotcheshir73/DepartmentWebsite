using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentModel.Models;
using DepartmentModel.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Xml;

namespace DepartmentService.BindingModels
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

    public class AcademicPlanRecrodsForDiciplineBindingModel : PageSettingBinidingModel
    {
        public Guid AcademicYearId { get; set; }

        public Guid DisciplineId { get; set; }
    }

    public class ScheduleRecordsForDiciplineBindingModel : PageSettingBinidingModel
    {
        public Guid SeasonDateId { get; set; }

        public Guid DisciplineId { get; set; }
    }
}
