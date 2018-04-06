using DepartmentModel;

namespace DepartmentService.IServices
{
    public interface IAdministrationProcess
    {
        ResultService CheckAllUsersStatus();

        ResultService CheckExsistData();

        ResultService SynchronizationRolesAndAccess();

        ResultService ImportDataToJson(string folderName);

        ResultService ExportDataFromJson(string folderName);

        ResultService CreateBackUp(string fileName);

        ResultService RestoreBackUp(string fileName);
    }
}
