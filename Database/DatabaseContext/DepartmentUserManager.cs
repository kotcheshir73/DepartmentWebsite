using Enums;
using Microsoft.EntityFrameworkCore;
using Models.AcademicYearData;
using Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DatabaseContext
{
    public class DepartmentUserManager
    {
        private static Encoding ascii = Encoding.ASCII;

        private static DepartmentUser _user;

        private static List<DepartmentRole> _roles;

        public static Guid? UserId
        {
            get { return _user?.Id; }
        }

        public static string LecturerName => $"{_user?.Lecturer?.LastName} {_user?.Lecturer?.FirstName[0]}.{_user?.Lecturer?.Patronymic?[0]}.";
        

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
                var access = context.DepartmentAccesses.FirstOrDefault(a => a.Operation == operation && _roles.Contains(a.Role));
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
        public static bool Login(string login, string password)
        {
            var passHash = GetPasswordHash(password);

            using (var context = GetContext)
            {
                var user = context.DepartmentUsers.FirstOrDefault(u => u.UserName == login && u.PasswordHash == passHash);
                if (user == null)
                {
                    throw new Exception("Введен неверный логин/пароль");
                }
                if (user.IsLocked)
                {
                    throw new Exception("Пользователь заблокирован");
                }
                user.DateLastVisit = DateTime.Now;
                context.SaveChanges();
                _user = user;
                _roles = context.DepartmentUserRoles.Where(x => x.UserId == _user.Id).Select(x => x.Role).ToList();
            }

            return true;
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static void LoginAsync(string login, string password)
        {
            var passHash = GetPasswordHash(password);

            using (var context = GetContext)
            {
                var user = context.DepartmentUsers.Where(x => x.LecturerId != null).Include(x => x.Lecturer).FirstOrDefault(u => u.UserName == login && u.PasswordHash == passHash);
                if (user == null)
                {
                    throw new Exception("Введен неверный логин/пароль");
                }
                if (user.IsLocked)
                {
                    throw new Exception("Пользователь заблокирован");
                }
                user.DateLastVisit = DateTime.Now;
                context.SaveChanges();
                _user = user;
                _roles = context.DepartmentUserRoles.Where(x => x.UserId == _user.Id).Select(x => x.Role).ToList();
            }

            return;
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
            DepartmentRole role = null;
            DepartmentUser user = null;
            using (var context = GetContext)
            using (var transaction = context.Database.BeginTransaction())
            {
                role = new DepartmentRole
                {
                    RoleName = "Администратор"
                };
                context.DepartmentRoles.Add(role);
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

                var md5 = new MD5CryptoServiceProvider();
                user = new DepartmentUser
                {
                    UserName = "admin",
                    PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes("qwerty")))
                };
                context.DepartmentUsers.Add(user);
                context.SaveChanges();

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