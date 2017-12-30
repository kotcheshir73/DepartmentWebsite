using DepartmentDAL;

namespace DepartmentService.IServices
{
    public interface IAdministrationProcessServer
    {
        ResultService CheckAllUsersStatus();

        ResultService ImportDataToJson(string folderName);

        ResultService ExportDataFromJson(string folderName);
    }
}
