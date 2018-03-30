using System;

namespace DepartmentService.BindingModels
{
    public class LaboratoryProcessMakeCloneBindingModel
    {
        public Guid Id { get; set; }
    }

    public class LaboratoryProcessApplyInfoByAnotherSoftwareReocrdsBindingModel
    {
        public Guid Id { get; set; }
    }

    public class LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel
    {
        public Guid ClassroomId { get; set; }

        public string ClaimNumber { get; set; }

        public string InventoryNumber { get; set; }
    }
}
