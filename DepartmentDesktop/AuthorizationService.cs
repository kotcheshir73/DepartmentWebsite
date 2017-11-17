using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDesktop
{
	public static class AuthorizationService
	{
		private static long? _userId;

		public static long? UserId { get { return _userId ?? 0; } }

		public static bool Login(string userName, string Password)
		{
			return true;
		}

		public static void Logout()
		{
			_userId = null;
		}
	}
}
