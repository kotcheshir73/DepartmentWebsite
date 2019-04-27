using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class IndividualPlanNIRScientificArticleGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? IndividualPlanId { get; set; }

        public string Status { get; set; }
    }

    public class IndividualPlanNIRScientificArticleSetBindingModel : PageSettingSetBinidingModel
    {
        public Guid IndividualPlanId { get; set; }
        
        public string Name { get; set; }

        public int Order { get; set; }

        public string TypeOfPublication { get; set; }
        
        public double Volume { get; set; }
        
        public string Publishing { get; set; }
        
        public int Year { get; set; }
        
        public string Status { get; set; }
    }
}