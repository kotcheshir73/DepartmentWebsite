using System;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.BindingModels
{
    public class LecturerPostGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }
    }

    public class LecturerPostRecordBindingModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "required")]
        public int Hours { get; set; }
    }
}
