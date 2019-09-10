using DatabaseContext;
using Models.Authentication;
using System;
using Tools;

namespace DepartmentTablet
{
    public static class AuthorizationService
    {
        private static DepartmentUser _user;

        public static DepartmentUser User { get { return _user ?? null; } }

        public static Guid? UserId { get { return _user?.Id ?? null; } }

        public static bool Login(string userName, string password)
        {
            DepartmentUserManager.Login(userName, password);
            //_user = AccessCheckService.Login(userName, password);
            return _user != null;
        }

        public static void Logout()
        {
            _user = null;
        }
    }
}
