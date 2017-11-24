using DepartmentDAL;
using DepartmentDAL.Enums;
using System.Collections.Generic;
using System.Xml;

namespace DepartmentService.BindingModels
{
	public class EducationalProcessLoadFromXMLBindingModel
	{
		public long Id { get; set; }

		public string FileName { get; set; }
	}

	public class ParseDisciplineBindingModel
	{
		public long AcademicPlanId { get; set; }

		public XmlNode Node { get; set; }

		public int Counter { get; set; }

		public string KafedraNumber { get; set; }

		public ResultService Result { get; set; }

		public long DisciplineBlockId { get; set; }

		public List<Semesters> Semesters { get; set; }
	}

	public class ParsePracticBindingModel
	{
		public long AcademicPlanId { get; set; }

		public long DisciplineBlockId { get; set; }

		public int Counter { get; set; }

		public string PracticName { get; set; }

		public XmlNode Node { get; set; }

		public string KafedraNumber { get; set; }

		public ResultService Result { get; set; }

		public List<Semesters> Semesters { get; set; }
	}

	public class ParseFinalBindingModel
	{
		public long AcademicPlanId { get; set; }

		public string AcademicLevel { get; set; }

		public long DisciplineBlockId { get; set; }

		public ResultService Result { get; set; }

		public XmlNode Node { get; set; }

		public int SemesterNumber { get; set; }
	}
}
