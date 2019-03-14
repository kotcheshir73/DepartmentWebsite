using System;

namespace DepartmentService.BindingModels
{
    public class GraficRecordGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? GraficId { get; set; }

        public Guid? TimeNormId { get; set; }
    }

    public class GraficRecordSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid GraficId { get; set; }

        public Guid TimeNormId { get; set; }

        public int WeekNumber { get; set; }

        public double Hours { get; set; }
    }
}
