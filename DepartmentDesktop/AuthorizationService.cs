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
		private static UserViewModel _user;

		public static UserViewModel User { get { return _user ?? null; } }

		public static long? UserId { get { return _user?.Id ?? null; } }

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
