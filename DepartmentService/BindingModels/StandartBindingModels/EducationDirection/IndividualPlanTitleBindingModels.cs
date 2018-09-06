using System;

namespace DepartmentService.BindingModels
{
    public class IndividualPlanTitleGetBindingModel : PageSettingBinidingModel
    {
        public Guid?Id { get; set; }
    }

    public class IndividualPlanTitleSetBindingModel
    {
        public Guid Id { get; set; }

        public string Title
        {
            get; set;
        }
    }
}
