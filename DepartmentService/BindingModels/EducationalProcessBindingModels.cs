using DepartmentModel;
using DepartmentModel.Enums;
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

    public class ParseDisciplineBindingModel
    {
        public Guid AcademicPlanId { get; set; }

        public XmlNode Node { get; set; }

        public int Counter { get; set; }

        public string KafedraNumber { get; set; }

        public ResultService Result { get; set; }

        public Guid DisciplineBlockId { get; set; }

        public List<Semesters> Semesters { get; set; }
    }

    public class ParsePracticBindingModel
    {
        public Guid AcademicPlanId { get; set; }

        public Guid DisciplineBlockId { get; set; }

        public int Counter { get; set; }

        public string PracticName { get; set; }

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
