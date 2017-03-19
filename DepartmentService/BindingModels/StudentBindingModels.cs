using DepartmentDAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class StudentGetBindingModel
    {
        public string NumberOfBook { get; set; }

        public long? StudentGroupId { get; set; }

		public StudentState? StudentStatus { get; set; }

		public int? PageNumber { get; set; }

		public int PageSize { get; set; }

		public int CountRecords { get; set; }
    }

    public class StudentHistoryGetBindingModel
    {
        public string NumberOfBook { get; set; }

        public long? Id { get; set; }
    }

    public class StudentLoadDocBindingModel
    {
        public long Id { get; set; }

        public string FileName { get; set; }
    }

    public class StudentRecordBindingModel
    {
        [Required(ErrorMessage = "required")]
        public string NumberOfBook { get; set; }

        [Required(ErrorMessage = "required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "required")]
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "required")]
        public string StudentStatus { get; set; }

        public long StudentGroupId { get; set; }

        public byte[] Photo { get; set; }
    }

	public class StudentEnrollmentBindingModel
	{
		public string OrderNumber { get; set; }

		public DateTime OrderDate { get; set; }

		public List<StudentRecordBindingModel> StudentList { get; set; }
	}

	public class StudentTransferBindingModel
	{
		public long NewStudentGroupId { get; set; }

		public long OldStudentGroupId { get; set; }

		public List<StudentRecordBindingModel> StudentList { get; set; }

		public DateTime TransferDate { get; set; }

		public string TransferReason { get; set; }
	}

	public class StudentDeductionBindingModel
	{
		public List<StudentRecordBindingModel> StudentList { get; set; }

		public long StudentGroupId { get; set; }

		public DateTime DeductionDate { get; set; }

		public string DeductionReason { get; set; }

		public string DeductionOrderNumber { get; set; }
	}

	public class StudentToAcademBindingModel
	{
		public List<StudentRecordBindingModel> StudentList { get; set; }

		public long StudentGroupId { get; set; }

		public DateTime ToAcademDate { get; set; }

		public string ToAcademReason { get; set; }

		public string ToAcademOrderNumber { get; set; }
	}

	public class StudentHistoryRecordBindingModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string NumberOfBook { get; set; }

        public DateTime DateCreate { get; set; }

        [Required(ErrorMessage = "required")]
        public string TextMessage { get; set; }
    }
}
