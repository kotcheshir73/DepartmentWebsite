using DepartmentModel;

namespace DepartmentService.IServices
{
    public interface IAdministrationProcessServer
    {
        ResultService CheckAllUsersStatus();

        ResultService ImportDataToJson(string folderName);

        ResultService ExportDataFromJson(string folderName);

        ResultService CreateBackUp(string fileName);

        ResultService RestoreBackUp(string fileName);
    }
}
