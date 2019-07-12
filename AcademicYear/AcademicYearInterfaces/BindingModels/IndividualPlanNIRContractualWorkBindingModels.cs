using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class IndividualPlanNIRContractualWorkGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? IndividualPlanId { get; set; }
    }

    public class IndividualPlanNIRContractualWorkSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid IndividualPlanId { get; set; }
        
        public string JobContent { get; set; }
        
        public string Post { get; set; }
        
        public string PlannedTerm { get; set; }
        
        public bool ReadyMark { get; set; }
    }
}