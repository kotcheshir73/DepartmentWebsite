using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class StudentGetBindingModel
    {
        public string NumberOfBook { get; set; }

        public long? StudentGroupId { get; set; }
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
