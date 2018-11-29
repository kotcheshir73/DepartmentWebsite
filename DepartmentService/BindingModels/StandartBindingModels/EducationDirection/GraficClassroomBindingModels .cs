using System;

namespace DepartmentService.BindingModels
{
    public class GraficClassroomGetBindingModel : PageSettingBinidingModel
    {
        public Guid? Id { get; set; }

        public Guid? GraficId { get; set; }

        public Guid? TimeNormId { get; set; }
    }

    public class GraficClassroomSetBindingModel
    {
        public Guid Id { get; set; }

        public Guid GraficId { get; set; }

        public Guid TimeNormId { get; set; }

        public string ClassroomDescription { get; set; }
    }
}
