using LaboratoryHeadInterfaces.BindingModels;
using LaboratoryHeadInterfaces.ViewModels;
using Tools;

namespace LaboratoryHeadInterfaces.Interfaces
{
    public interface ILaboratoryProcess
    {
        ResultService MakeClone(LaboratoryProcessMakeCloneBindingModel model);

        ResultService ApplyMTVRecords(LaboratoryProcessApplyMTVRecordsBindingModel model);

        ResultService ApplyInfoByAnotherSoftwareReocrds(LaboratoryProcessApplyInfoByAnotherSoftwareReocrdsBindingModel model);

        ResultService<LaboratoryProcessSoftwareRecordPageViewModel> GetSoftwareRecordsByClassrooms(LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel model);

        ResultService<LaboratoryProcessSoftwareRecordPageViewModel> GetSoftwareRecordsByClaimNumber(LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel model);

        ResultService<LaboratoryProcessSoftwareRecordPageViewModel> GetSoftwareRecordsByInventoryNumber(LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel model);

        ResultService<SoftwarePageViewModel> GetSoftwareByInvNumbers(LaboratoryProcessGetSoftwareByInvNumbersBindingModel model);

        ResultService InstallSoftware(LaboratoryProcessInstalSoftwareBindingModel model);

        ResultService UnInstallSoftware(LaboratoryProcessUnInstalSoftwareBindingModel model);
    }
}