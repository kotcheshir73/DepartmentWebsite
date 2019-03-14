using System;

namespace DepartmentService.BindingModels
{
    public class IndividualPlanNIRContractualWorkGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? LecturerId { get; set; }
    }

    public class IndividualPlanNIRContractualWorkSetBindingModel
    {
        public Guid Id { get; set; }
        
        public Guid LecturerId { get; set; }
        
        public string JobContent { get; set; }
        
        public string Post { get; set; }
        
        public string PlannedTerm { get; set; }
        
        public string ReadyMark { get; set; }
    }
}
