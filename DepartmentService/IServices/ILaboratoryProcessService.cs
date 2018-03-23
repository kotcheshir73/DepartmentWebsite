using DepartmentModel;
using DepartmentService.BindingModels;

namespace DepartmentService.IServices
{
    public interface ILaboratoryProcessService
    {
        ResultService MakeClone(LaboratoryProcessMakeCloneBindingModels model);
    }
}
