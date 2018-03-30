using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;

namespace DepartmentService.IServices
{
    public interface ILaboratoryProcess
    {
        ResultService MakeClone(LaboratoryProcessMakeCloneBindingModel model);

        ResultService ApplyInfoByAnotherSoftwareReocrds(LaboratoryProcessApplyInfoByAnotherSoftwareReocrdsBindingModel model);

        ResultService<LaboratoryProcessSoftwareRecordPageViewModel> GetSoftwareRecordsByClassrooms(LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel model);

        ResultService<LaboratoryProcessSoftwareRecordPageViewModel> GetSoftwareRecordsByClaimNumber(LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel model);

        ResultService<LaboratoryProcessSoftwareRecordPageViewModel> GetSoftwareRecordsByInventoryNumber(LaboratoryProcessGetSoftwareRecordsByClassroomBindingModel model);
    }
}
