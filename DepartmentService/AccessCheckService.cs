using DepartmentDAL.Context;
using DepartmentDAL.Enums;
using DepartmentDAL.Models;
using DepartmentService.ViewModels;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DepartmentService
{
	/// <summary>
	/// Класс для проверки доступа пользователя к сервису
	/// </summary>
	public static class AccessCheckService
	{
		private static readonly DepartmentDbContext _context = new DepartmentDbContext();

		private static Encoding ascii = Encoding.ASCII;

		/// <summary>
		/// Авторизация пользователя к операции
		/// </summary>
		/// <param name="operation"></param>
		/// <param name="type"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		public static bool CheckAccess(AccessOperation operation, AccessType type, long userId)
		{
			var roleId = _context.Users.SingleOrDefault(u => u.Id == userId)?.RoleId ?? -0;

			if (roleId > 0)
			{
				var access = _context.Accesses.FirstOrDefault(a => a.Operation == operation && a.RoleId == roleId);
				if(access != null)
				{
					return access.AccessType <= type;
				}
			}
			// TODO как настроят роли и пользователи - заменить на false
			return true;
		}

		/// <summary>
		/// Аутентификация пользователя
		/// </summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public static UserViewModel Login(string login, string password)
		{
			var passHash = GetPasswordHash(password);
			var user = _context.Users.SingleOrDefault(u => u.Login == login && u.Password == passHash);
			if (user == null)
			{
				throw new Exception("Введен неверный логин/пароль");
			}
			if (user.IsBanned)
			{
				throw new Exception("Пользователь забаннен");
			}
			user.DateLastVisit = DateTime.Now;
			_context.SaveChanges();
			return ModelFactoryToViewModel.CreateUserViewModel(user);
		}

		/// <summary>
		/// Смена пароля
		/// </summary>
		/// <param name="login"></param>
		/// <param name="oldPassword"></param>
		/// <param name="newPassword"></param>
		public static void ChangePassword(string login, string oldPassword, string newPassword)
		{
			var passHash = GetPasswordHash(oldPassword);
			var user = _context.Users.SingleOrDefault(u => u.Login == login && u.Password == passHash);
			if (user == null)
			{
				throw new Exception("Введен неверный логин/пароль");
			}
			if (user.IsBanned)
			{
				throw new Exception("Пользователь забаннен");
			}
			user.Password = GetPasswordHash(newPassword);
			_context.SaveChanges();
		}

		/// <summary>
		/// Получение хеша пароля
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		public static string GetPasswordHash(string password)
		{
			var md5 = new MD5CryptoServiceProvider();
			return ascii.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(password)));
		}
	}
}
