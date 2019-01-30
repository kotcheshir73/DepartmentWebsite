using DepartmentService.BindingModels;
using System;

namespace ScheduleServiceInterfaces.BindingModels
{
    public class ScheduleRecordsForDiciplineBindingModel : PageSettingBinidingModel
    {
        public Guid SeasonDateId { get; set; }

        public Guid DisciplineId { get; set; }
    }
}