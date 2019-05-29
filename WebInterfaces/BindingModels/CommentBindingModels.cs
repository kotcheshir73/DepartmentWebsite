using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tools.BindingModels;

namespace WebInterfaces.BindingModels
{
    public class CommentGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? ParentId { get; set; }

        public Guid? EventId { get; set; }

        public Guid? DisciplineId { get; set; }

    }

    public class CommentSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public string Content { get; set; }

        public Guid? DisciplineId { get; set; }

        public Guid? EventId { get; set; }

        public Guid? ParentId { get; set; }

        public string DepartmentUser { get; set; }
                
    }
}
