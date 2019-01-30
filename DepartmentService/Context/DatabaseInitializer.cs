using DepartmentModel.Enums;
using DepartmentModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;

namespace DepartmentService.Context
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
            DepartmentUserManager userManager = new DepartmentUserManager(new DepartmentUserStore(context));
            using (var transaction = context.Database.BeginTransaction())
            {
                DepartmentRole role = new DepartmentRole
                {
                    Name = "Администратор"
                };
                context.Roles.Add(role);

                context.SaveChanges();

                var md5 = new MD5CryptoServiceProvider();
                DepartmentUser user = new DepartmentUser
                {
                    UserName = "admin",
                    PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes("qwerty")))
                };

                context.Users.Add(user);

                context.SaveChanges();

                context.SaveChanges();

                userManager.AddToRoleAsync(user.Id, role.Name).Wait();

                List<DepartmentAccess> accesses = new List<DepartmentAccess>();
                foreach (AccessOperation elem in Enum.GetValues(typeof(AccessOperation)))
                {
                    accesses.Add(new DepartmentAccess
                    {
                        AccessType = AccessType.Administrator,
                        Operation = elem,
                        RoleId = role.Id
                    });
                }

                context.Accesses.AddRange(accesses);

                context.SaveChanges();

                transaction.Commit();
            }
        }
    }
}
