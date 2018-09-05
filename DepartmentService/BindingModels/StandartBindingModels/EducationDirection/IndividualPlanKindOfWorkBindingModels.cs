using System;

namespace DepartmentService.BindingModels
{
    public class IndividualPlanKindOfWorkGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? IndividualPlanTitleId { get; set; }
    }

    public class IndividualPlanKindOfWorkSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid IndividualPlanTitleId { get; set; }

        public string Name { get; set; }

        public string TimeNormDescription { get; set; }
    }
}
