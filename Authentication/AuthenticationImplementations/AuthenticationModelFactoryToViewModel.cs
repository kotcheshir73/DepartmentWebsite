using AuthenticationInterfaces.ViewModels;
using Models.Authentication;
using System.Drawing;
using System.IO;

namespace AuthenticationServiceImplementations
{
    public static class AuthenticationModelFactoryToViewModel
    {
        public static RoleViewModel CreateRoleViewModel(DepartmentRole entity)
        {
            return new RoleViewModel
            {
                Id = entity.Id,
                RoleName = entity.RoleName
            };
        }

        public static AccessViewModel CreateAccessViewModel(DepartmentAccess entity)
        {
            return new AccessViewModel
            {
                Id = entity.Id,
                RoleName = entity.Role.RoleName,
                Operation = entity.Operation.ToString(),
                AccessType = entity.AccessType.ToString()
            };
        }

        public static UserViewModel CreateUserViewModel(DepartmentUser entity)
        {
            return new UserViewModel
            {
                Id = entity.Id,
                Login = entity.UserName,
                StudentId = entity.StudentId,
                LecturerId = entity.LecturerId,
                Avatar = entity.Avatar != null && entity.Avatar.Length > 0 ? Image.FromStream(new MemoryStream(entity.Avatar)) : null,
                IsBanned = entity.IsLocked,
                DateBanned = entity.DateBanned,
                DateLastVisit = entity.DateLastVisit
            };
        }
    }
}