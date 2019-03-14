using AuthenticationModels.Models;
using AuthenticationServiceInterfaces.ViewModels;
using DepartmentService;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDesktop
{
	public static class AuthorizationService
	{
		private static DepartmentUser _user;

		public static DepartmentUser User { get { return _user ?? null; } }

		public static Guid? UserId { get { return _user?.Id ?? null; } }

		public static bool Login(string userName, string password)
		{
            _user = AccessCheckService.Login(userName, password);
			return _user != null;
		}

		public static void Logout()
		{
			_user = null;
		}
	}
}
