using Tools;

namespace AuthenticationInterfaces.Interfaces
{
    public interface IAuthenticationProcess
    {
        ResultService ImportDataToJson(string folderName);

        ResultService ExportDataFromJson(string folderName);

        ResultService CreateBackUp(string fileName);

        ResultService RestoreBackUp(string fileName);

        ResultService SynchronizationRolesAndAccess();

        ResultService SynchronizationUsers();
    }
}