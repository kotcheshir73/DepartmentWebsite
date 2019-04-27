using System;
using Tools.BindingModels;

namespace AcademicYearInterfaces.BindingModels
{
    public class IndividualPlanRecordGetBindingModel : PageSettingGetBinidingModel
    {
        public Guid? IndividualPlanKindOfWorkId { get; set; }

        public Guid? IndividualPlanId { get; set; }

        public string Title { get; set; }
    }

    public class IndividualPlanRecordSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid IndividualPlanKindOfWorkId { get; set; }

        public Guid IndividualPlanId { get; set; }

        public double PlanAutumn { get; set; }

        public double FactAutumn { get; set; }

        public double PlanSpring { get; set; }

        public double FactSpring { get; set; }
    }
}