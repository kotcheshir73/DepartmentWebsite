using DepartmentModel;

namespace AuthenticationServiceInterfaces.Interfaces
{
    public interface IAdministrationProcess
    {
        ResultService CheckExsistData();

        ResultService SynchronizationRolesAndAccess();

        ResultService SynchronizationUsers();

        ResultService ImportDataToJson(string folderName);

        ResultService ExportDataFromJson(string folderName);

        ResultService CreateBackUp(string fileName);

        ResultService RestoreBackUp(string fileName);
    }
}