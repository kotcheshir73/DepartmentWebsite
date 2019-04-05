using Models.Authentication;
using Models.Enums;
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

        public static DatabaseContext GetContext
        {
            get
            {
                return new DatabaseContext();
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
            using (var context = new DatabaseContext())
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

            using (var context = new DatabaseContext())
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
        /// Смена пароля
        /// </summary>
        /// <param name="login"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        public static void ChangePassword(string login, string oldPassword, string newPassword)
        {
            var passHash = GetPasswordHash(oldPassword);
            using (var context = new DatabaseContext())
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
    }
}