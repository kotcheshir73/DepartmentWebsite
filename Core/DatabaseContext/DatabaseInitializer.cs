using Models.Authentication;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DatabaseContext
{
    public class DatabaseInitializer
    {
        /// <summary>
        /// Инициализация БД, добавление роли Администратора, пользователя admin и прав для него
        /// </summary>
        /// <param name="context"></param>
        public void Seed(DatabaseContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                DepartmentRole role = new DepartmentRole
                {
                    RoleName = "Администратор"
                };
                context.DepartmentRoles.Add(role);

                context.SaveChanges();

                var md5 = new MD5CryptoServiceProvider();
                DepartmentUser user = new DepartmentUser
                {
                    UserName = "admin",
                    PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes("qwerty")))
                };

                context.DepartmentUsers.Add(user);

                context.SaveChanges();

                context.SaveChanges();

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

                context.DepartmentAccesses.AddRange(accesses);

                context.SaveChanges();

                transaction.Commit();
            }
        }
    }
}