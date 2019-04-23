using System;
using System.Collections.Generic;

namespace LaboratoryHeadInterfaces.BindingModels
{
    public class LaboratoryProcessMakeCloneBindingModel
    {
        public Guid Id { get; set; }
    }

    public class LaboratoryProcessApplyMTVRecordsBindingModel
    {
        public Guid Id { get; set; }

        public List<Guid> ApllyIds { get; set; }
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

    public class LaboratoryProcessGetSoftwareByInvNumbersBindingModel
    {
        public List<string> InventoryNumbers { get; set; }
    }

    public class LaboratoryProcessInstalSoftwareBindingModel
    {
        public List<string> SoftwareNames { get; set; }

        public List<string> InventoryNumbers { get; set; }

        public DateTime DateSetup { get; set; }

        public string SetupDescription { get; set; }

        public string ClaimNumber { get; set; }
    }

    public class LaboratoryProcessUnInstalSoftwareBindingModel
    {
        public List<string> SoftwareNames { get; set; }

        public List<string> InventoryNumbers { get; set; }

        public DateTime DateDelete { get; set; }

        public string DeleteReason { get; set; }
    }
}