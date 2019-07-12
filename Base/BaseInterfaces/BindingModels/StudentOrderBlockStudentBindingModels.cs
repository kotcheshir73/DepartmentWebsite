using System;
using System.ComponentModel.DataAnnotations;
using Tools.BindingModels;

namespace BaseInterfaces.BindingModels
{
    public class StudentOrderBlockStudentGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? StudentOrderBlockId { get; set; }

        public Guid? StudentOrderId { get; set; }

        public Guid? StudentId { get; set; }

        public Guid? StudentGroupFromId { get; set; }

        public Guid? StudentGroupToId { get; set; }
    }

    public class StudentOrderBlockStudentSetBindingModel : PageSettingSetBinidingModel
    {
        [Required(ErrorMessage = "required")]
        public Guid StudentOrderBlockId { get; set; }

        [Required(ErrorMessage = "required")]
        public Guid StudentId { get; set; }
        
        public Guid? StudentGroupFromId { get; set; }
        
        public Guid? StudentGroupToId { get; set; }

        public string Info { get; set; }
    }
}