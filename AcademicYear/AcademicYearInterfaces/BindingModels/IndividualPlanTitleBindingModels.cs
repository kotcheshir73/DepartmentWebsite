using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class IndividualPlanTitleGetBindingModel : PageSettingGetBinidingModel
    {
        public string Title { get; set; }
    }

    public class IndividualPlanTitleSetBindingModel : PageSettingSetBinidingModel
    {
        public string Title { get; set; }

        public int Order { get; set; }
    }
}