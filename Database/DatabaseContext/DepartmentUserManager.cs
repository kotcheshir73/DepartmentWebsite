using Enums;
using Microsoft.EntityFrameworkCore;
using Models.AcademicYearData;
using Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseContext
{
    public class DepartmentUserManager
    {
        private static readonly Encoding ascii = Encoding.ASCII;
        private static readonly int CountMaxAttempt = 3;

        public static Guid? UserId => User?.Id;

        public static bool IsAuth => User != null;

        public static string LecturerName => $"{User?.Lecturer?.LastName} {User?.Lecturer?.FirstName[0]}.{User?.Lecturer?.Patronymic?[0]}.";        

        public static DepartmentDatabaseContext GetContext
        {
            get
            {
                return new DepartmentDatabaseContext();
            }
        }

        public static SeasonDates GetCurrentDates()
        {
            using (var context = GetContext)
            {
                var currentSetting = context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Даты семестра");
                if (currentSetting == null)
                {
                    throw new Exception("CurrentSetting not found");
                }

                var currentDates = context.SeasonDates.Where(sd => sd.Title == currentSetting.Value).FirstOrDefault();
                if (currentDates == null)
                {
                    throw new Exception("CurrentDates not found");
                }
                return currentDates;
            }
        }

        public static DepartmentUser User { get; private set; }

        public static List<DepartmentRole> Roles { get; private set; }

        /// <summary>
        /// Авторизация пользователя к операции
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static void CheckAccess(AccessOperation operation, AccessType type, string entity)
        {
            using (var context = GetContext)
            {
                var access = context.DepartmentAccesses.FirstOrDefault(a => a.Operation == operation && Roles.Contains(a.Role));
                if (access != null)
                {
                    if (access.AccessType >= type) return;
                }
                switch (type)
                {
                    case AccessType.View:
                        throw new Exception($"Нет доступа на чтение данных по сущности '{entity}'");
                    case AccessType.Change:
                        throw new Exception($"Нет доступа на изменение данных по сущности '{entity}'");
                    case AccessType.Delete:
                        throw new Exception($"Нет доступа на удаление данных по сущности '{entity}'");
                }
            }
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task LoginAsync(string login, string password)
        {
            var passHash = GetPasswordHash(password);

            using (var context = GetContext)
            {
                var user = await context.DepartmentUsers.FirstOrDefaultAsync(u => u.UserName == login && u.PasswordHash == passHash);
                if (user == null)
                {
                    var checkUser = await context.DepartmentUsers.FirstOrDefaultAsync(u => u.UserName == login && !u.IsLocked);
                    if (checkUser != null)
                    {
                        checkUser.CountAttempt++;
                        if (checkUser.CountAttempt > CountMaxAttempt)
                        {
                            checkUser.IsLocked = true;
                            checkUser.DateLocked = DateTime.Now;
                        }
                    }

                    throw new Exception("Введен неверный логин/пароль");
                }
                if (user.IsLocked)
                {
                    if (user.DateLocked.Value.AddHours(3) > DateTime.Now)
                    {
                        user.IsLocked = false;
                    }
                    else
                    {
                        throw new Exception("Пользователь заблокирован");
                    }
                }

                user.DateLastVisit = DateTime.Now;
                await context.SaveChangesAsync();
                User = user;
                Roles = await context.DepartmentUserRoles.Where(x => x.UserId == User.Id).Select(x => x.Role).ToListAsync();
            }
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
            using (var context = GetContext)
            {
                var user = context.DepartmentUsers.SingleOrDefault(u => u.UserName == login && u.PasswordHash == passHash);
                if (user == null)
                {
                    throw new Exception("Введен неверный логин/пароль");
                }
                if (user.IsLocked)
                {
                    throw new Exception("Пользователь забаннен");
                }
                user.PasswordHash = GetPasswordHash(newPassword);
                context.SaveChanges();
            }
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

        public static void CheckExsistData()
        {
            try
            {
                using (var context = GetContext)
                {
                    var role = context.DepartmentRoles.FirstOrDefault(x => x.RoleName == "Администратор");
                    if (role == null)
                    {
                        CreateAdministrationRoleAndUserWithAllAccess();
                    }
                    role = context.DepartmentRoles.FirstOrDefault(x => x.RoleName == "Преподаватель");
                    if (role == null)
                    {
                        CreateLecturerRolesWithAllAccess();
                    }
                    role = context.DepartmentRoles.FirstOrDefault(x => x.RoleName == "Студент");
                    if (role == null)
                    {
                        CreateStudentRolesWithAllAccess();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void CreateAdministrationRoleAndUserWithAllAccess()
        {
            using (var context = GetContext)
            using (var transaction = context.Database.BeginTransaction())
            {
                DepartmentRole role = new DepartmentRole
                {
                    RoleName = "Администратор"
                };
                context.DepartmentRoles.Add(role);

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

                var md5 = new MD5CryptoServiceProvider();
                DepartmentUser user = new DepartmentUser
                {
                    UserName = "admin",
                    PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes("qwerty")))
                };
                context.DepartmentUsers.Add(user);

                context.DepartmentUserRoles.Add(new DepartmentUserRole { UserId = user.Id, RoleId = role.Id });

                context.SaveChanges();
                transaction.Commit();
            }
        }

        private static void CreateLecturerRolesWithAllAccess()
        {
            using (var context = GetContext)
            using (var transaction = context.Database.BeginTransaction())
            {
                DepartmentRole role = new DepartmentRole
                {
                    RoleName = "Преподаватель"
                };
                context.DepartmentRoles.Add(role);
                context.SaveChanges();

                //List<Access> accesses = new List<Access>();
                //foreach (AccessOperation elem in Enum.GetValues(typeof(AccessOperation)))
                //{
                //    accesses.Add(new Access
                //    {
                //        AccessType = AccessType.Administrator,
                //        Operation = elem,
                //        RoleId = role.Id
                //    });
                //}
                //_context.Accesses.AddRange(accesses);
                //_context.SaveChanges();

                transaction.Commit();
            }
        }

        private static void CreateStudentRolesWithAllAccess()
        {
            using (var context = GetContext)
            using (var transaction = context.Database.BeginTransaction())
            {
                DepartmentRole role = new DepartmentRole
                {
                    RoleName = "Студент"
                };
                context.DepartmentRoles.Add(role);
                context.SaveChanges();

                //List<Access> accesses = new List<Access>();
                //foreach (AccessOperation elem in Enum.GetValues(typeof(AccessOperation)))
                //{
                //    accesses.Add(new Access
                //    {
                //        AccessType = AccessType.Administrator,
                //        Operation = elem,
                //        RoleId = role.Id
                //    });
                //}
                //_context.Accesses.AddRange(accesses);
                //_context.SaveChanges();

                transaction.Commit();
            }
        }
    }
}