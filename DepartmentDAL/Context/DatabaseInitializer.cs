using DepartmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;

namespace DepartmentDAL.Context
{
	public class DatabaseInitializer : CreateDatabaseIfNotExists<DepartmentDbContext>
	{
		/// <summary>
		/// Инициализация БД, добавление роли Администратора, пользователя admin и прав для него
		/// </summary>
		/// <param name="context"></param>
		protected override void Seed(DepartmentDbContext context)
		{
			base.Seed(context);

			using (var transaction = context.Database.BeginTransaction())
			{
				Role role = new Role
				{
					DateCreate = DateTime.Now,
					RoleName = "Administrator"
				};
				context.Roles.Add(role);

				context.SaveChanges();

				var md5 = new MD5CryptoServiceProvider();
				User user = new User
				{
					DateCreate = DateTime.Now,
					Login = "admin",
					Password = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes("qwerty"))),
					RoleId = role.Id
				};

				context.Users.Add(user);

				context.SaveChanges();

				List<Access> accesses = new List<Access>{
					new Access
					{
						DateCreate = DateTime.Now,
						AccessType = Enums.AccessType.Administrator,
						Operation = Enums.AccessOperation.Администрирование,
						RoleId = role.Id
					},
					new Access
					{
						DateCreate = DateTime.Now,
						AccessType = Enums.AccessType.Administrator,
						Operation = Enums.AccessOperation.Доступы,
						RoleId = role.Id
					},
					new Access
					{
						DateCreate = DateTime.Now,
						AccessType = Enums.AccessType.Administrator,
						Operation = Enums.AccessOperation.Роли,
						RoleId = role.Id
					},
					new Access
					{
						DateCreate = DateTime.Now,
						AccessType = Enums.AccessType.Administrator,
						Operation = Enums.AccessOperation.Пользователи,
						RoleId = role.Id
					}
				};

				context.Accesses.AddRange(accesses);

				context.SaveChanges();

				transaction.Commit();
			}
		}
	}
}
