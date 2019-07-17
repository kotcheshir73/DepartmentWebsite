using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace WebInterfaces.BindingModels
{
    public class CommentGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? DepartmentUserId { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? NewsId { get; set; }

        public Guid? DisciplineId { get; set; }
    }

    public class CommentSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public Guid DepartmentUserId { get; set; }

        public Guid? DisciplineId { get; set; }

        public Guid? NewsId { get; set; }

        public Guid? ParentId { get; set; }

        [Required(ErrorMessage = "required")]
        public string Content { get; set; }
    }
}