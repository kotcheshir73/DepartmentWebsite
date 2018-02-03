using System;
using System.Collections.Generic;

namespace DepartmentService.BindingModels
{
    /// <summary>
    /// Загрузка студентов из файла
    /// </summary>
    public class StudentLoadDocBindingModel
	{
		public Guid Id { get; set; }

		public string FileName { get; set; }
	}

	/// <summary>
	/// Спсиок студентов на зачисление, + приказ
	/// </summary>
	public class StudentEnrollmentBindingModel
	{
		public string OrderNumber { get; set; }

		public DateTime OrderDate { get; set; }

		public List<StudentRecordBindingModel> StudentList { get; set; }
	}

	/// <summary>
	/// Спсиок студентов на перевод из одной группы в другую + причина перевода (включая восстановление после академа)
	/// </summary>
	public class StudentTransferBindingModel
	{
		public Guid? NewStudentGroupId { get; set; }

		public Guid? OldStudentGroupId { get; set; }

		public List<StudentRecordBindingModel> StudentList { get; set; }

		public DateTime TransferDate { get; set; }

		public string TransferReason { get; set; }
	}

	/// <summary>
	/// Спсиок студентов на отчисление + приказ
	/// </summary>
	public class StudentDeductionBindingModel
	{
		public List<StudentRecordBindingModel> StudentList { get; set; }

		public Guid StudentGroupId { get; set; }

		public DateTime DeductionDate { get; set; }

		public string DeductionReason { get; set; }

		public string DeductionOrderNumber { get; set; }
	}

	/// <summary>
	/// Спсиок студентов на перевод в академ + приказ
	/// </summary>
	public class StudentToAcademBindingModel
	{
		public List<StudentRecordBindingModel> StudentList { get; set; }

		public Guid StudentGroupId { get; set; }

		public DateTime ToAcademDate { get; set; }

		public string ToAcademReason { get; set; }

		public string ToAcademOrderNumber { get; set; }
	}
}
