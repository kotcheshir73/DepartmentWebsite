using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class ClassroomGetBindingModel
	{
		public string Id { get; set; }

		public long? UserId { get; set; }

		public int? PageNumber { get; set; }

		public int? PageSize { get; set; }
	}

    public class ClassroomRecordBindingModel
    {
        public string Id { get; set; }

		public long? UserId { get; set; }

		[Required(ErrorMessage = "required")]
        public string ClassroomType { get; set; }

        [Required(ErrorMessage = "required")]
        public int Capacity { get; set; }
    }
}
