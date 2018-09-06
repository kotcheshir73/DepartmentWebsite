using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
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
