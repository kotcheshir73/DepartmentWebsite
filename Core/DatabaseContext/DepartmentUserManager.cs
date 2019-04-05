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

        public static void SynchronizationRolesAndAccess()
        {
            try
            {
                CheckAdministrationAccesses();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SynchronizationUsers()
        {
            try
            {
                CheckAdminUsers();

                CheckTeacherUsers();

                CheckStudentUsers();
            }
            catch (Exception)
            {
                throw;
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

        private static void CheckAdministrationAccesses()
        {
            using (var context = GetContext)
            {
                var role = context.DepartmentRoles.FirstOrDefault(x => x.RoleName == "Администратор");
                if (role == null)
                {
                    throw new Exception("Остуствует роль \"Администратор\"");
                }
                using (var transaction = context.Database.BeginTransaction())
                {
                    List<DepartmentAccess> accesses = new List<DepartmentAccess>();
                    var accessInBD = context.DepartmentAccesses.Where(x => x.RoleId == role.Id).ToList();
                    foreach (AccessOperation elem in Enum.GetValues(typeof(AccessOperation)))
                    {
                        if (!accessInBD.Exists(x => x.Operation == elem))
                        {
                            accesses.Add(new DepartmentAccess
                            {
                                AccessType = AccessType.Administrator,
                                Operation = elem,
                                RoleId = role.Id
                            });
                        }
                        else if (!accessInBD.Exists(x => x.Operation == elem && x.AccessType == AccessType.Administrator))
                        {
                            var access = context.DepartmentAccesses.FirstOrDefault(x => x.Operation == elem && x.RoleId == role.Id);
                            access.AccessType = AccessType.Administrator;
                            context.SaveChanges();
                        }
                    }

                    context.DepartmentAccesses.AddRange(accesses);

                    context.SaveChanges();

                    transaction.Commit();
                }
            }
        }

        private static void CheckAdminUsers()
        {
            //TODO Все админы должны быть зарегестрированными в системе пользователями с ролью - админ
        }

        private static void CheckTeacherUsers()
        {
            using (var context = GetContext)
            using (var transaction = context.Database.BeginTransaction())
            {
                var role = context.DepartmentRoles.FirstOrDefault(x => x.RoleName == "Преподаватель");
                if (role == null)
                {
                    throw new Exception("Отсутствует роль \"Преподаватель\"");
                }
                var md5 = new MD5CryptoServiceProvider();
                #region Действующие преподаватели
                var lecturers = context.Lecturers.Where(x => !x.IsDeleted).ToList();
                foreach (var lecturer in lecturers)
                {
                    var user = context.DepartmentUsers.FirstOrDefault(x => x.LecturerId == lecturer.Id);
                    if (user == null)
                    {
                        user = new DepartmentUser
                        {
                            UserName = lecturer.ToString(),
                            PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(lecturer.ToString()))),
                            LecturerId = lecturer.Id
                        };
                        context.DepartmentUsers.Add(user);
                        context.SaveChanges();
                        context.DepartmentUserRoles.Add(new DepartmentUserRole { UserId = user.Id, RoleId = role.Id });
                        context.SaveChanges();
                    }
                    else
                    {
                        var userRole = context.DepartmentUserRoles.FirstOrDefault(x => x.UserId == user.Id && x.RoleId == role.Id);
                        if (userRole == null)
                        {
                            context.DepartmentUserRoles.Add(new DepartmentUserRole { UserId = user.Id, RoleId = role.Id });
                            context.SaveChanges();
                        }
                        else if (userRole.IsDeleted)
                        {
                            userRole.IsDeleted = false;
                            context.SaveChanges();
                        }
                        user.IsLocked = false;
                        user.DateBanned = null;
                        user.IsDeleted = false;
                        user.DateDelete = null;
                        context.SaveChanges();
                    }
                }
                #endregion
                #region Удаленные преподаватели
                var delLecturers = context.Lecturers.Where(x => x.IsDeleted).ToList();
                foreach (var lecturer in delLecturers)
                {
                    var user = context.DepartmentUsers.FirstOrDefault(x => x.LecturerId == lecturer.Id);
                    if (user != null)
                    {
                        if (!user.IsDeleted)
                        {
                            user.IsDeleted = true;
                            user.DateDelete = DateTime.Now;
                            context.SaveChanges();
                        }
                    }
                }
                #endregion
                transaction.Commit();
            }
        }

        private static void CheckStudentUsers()
        {
            using (var context = GetContext)
            using (var transaction = context.Database.BeginTransaction())
            {
                var role = context.DepartmentRoles.FirstOrDefault(x => x.RoleName == "Студент");
                if (role == null)
                {
                    throw new Exception("Отсутствует роль \"Студент\"");
                }
                var md5 = new MD5CryptoServiceProvider();
                #region Действующие студенты
                var students = context.Students.Where(x => !x.IsDeleted && x.StudentState == StudentState.Учится).ToList();
                foreach (var student in students)
                {
                    var user = context.DepartmentUsers.FirstOrDefault(x => x.StudentId == student.Id);
                    if (user == null)
                    {
                        user = new DepartmentUser
                        {
                            UserName = student.ToString(),
                            PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(student.ToString()))),
                            StudentId = student.Id
                        };
                        context.DepartmentUsers.Add(user);
                        context.SaveChanges();

                        context.DepartmentUserRoles.Add(new DepartmentUserRole { UserId = user.Id, RoleId = role.Id });
                    }
                    else
                    {
                        var userRole = context.DepartmentUserRoles.FirstOrDefault(x => x.UserId == user.Id && x.RoleId == role.Id);
                        if (userRole == null)
                        {
                            context.DepartmentUserRoles.Add(new DepartmentUserRole { UserId = user.Id, RoleId = role.Id });
                            context.SaveChanges();
                        }
                        else if (userRole.IsDeleted)
                        {
                            userRole.IsDeleted = false;
                            context.SaveChanges();
                        }
                        user.IsLocked = false;
                        user.DateBanned = null;
                        user.IsDeleted = false;
                        user.DateDelete = null;
                        context.SaveChanges();
                    }
                }
                #endregion
                #region Студенты отчисленные или в академе
                var bannedStudents = context.Students.Where(x => !x.IsDeleted && (x.StudentState == StudentState.Академ || x.StudentState == StudentState.Отчислен)).ToList();
                foreach (var student in bannedStudents)
                {
                    var user = context.DepartmentUsers.FirstOrDefault(x => x.StudentId == student.Id);
                    if (user == null)
                    {
                        user = new DepartmentUser
                        {
                            UserName = student.ToString(),
                            PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(student.ToString()))),
                            StudentId = student.Id
                        };
                        context.DepartmentUsers.Add(user);
                        context.SaveChanges();

                        context.DepartmentUserRoles.Add(new DepartmentUserRole { UserId = user.Id, RoleId = role.Id });
                    }
                    else
                    {
                        var userRole = context.DepartmentUserRoles.FirstOrDefault(x => x.UserId == user.Id && x.RoleId == role.Id);
                        if (userRole == null)
                        {
                            context.DepartmentUserRoles.Add(new DepartmentUserRole { UserId = user.Id, RoleId = role.Id });
                            context.SaveChanges();
                        }
                        else if (userRole.IsDeleted)
                        {
                            userRole.IsDeleted = false;
                            context.SaveChanges();
                        }
                    }
                    if (!user.IsLocked)
                    {
                        user.IsLocked = false;
                        user.DateBanned = DateTime.Now;
                    }
                    user.IsDeleted = false;
                    user.DateDelete = null;
                    context.SaveChanges();
                }
                #endregion
                #region Студенты, завершившие обучение
                var finishStudents = context.Students.Where(x => !x.IsDeleted && x.StudentState == StudentState.Завершил).ToList();
                foreach (var student in finishStudents)
                {
                    var user = context.DepartmentUsers.FirstOrDefault(x => x.StudentId == student.Id);
                    if (user == null)
                    {
                        user = new DepartmentUser
                        {
                            UserName = student.ToString(),
                            PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(student.ToString()))),
                            StudentId = student.Id
                        };
                        context.DepartmentUsers.Add(user);
                        context.SaveChanges();

                        context.DepartmentUserRoles.Add(new DepartmentUserRole { UserId = user.Id, RoleId = role.Id });
                    }
                    else
                    {
                        var userRole = context.DepartmentUserRoles.FirstOrDefault(x => x.UserId == user.Id && x.RoleId == role.Id);
                        if (userRole == null)
                        {
                            context.DepartmentUserRoles.Add(new DepartmentUserRole { UserId = user.Id, RoleId = role.Id });
                            context.SaveChanges();
                        }
                        else if (userRole.IsDeleted)
                        {
                            userRole.IsDeleted = false;
                            context.SaveChanges();
                        }
                    }
                    if (!user.IsDeleted)
                    {
                        user.IsDeleted = false;
                        user.DateDelete = null;
                        context.SaveChanges();
                    }
                }
                #endregion
                #region Удаленные студенты
                var delStudents = context.Students.Where(x => x.IsDeleted).ToList();
                foreach (var student in delStudents)
                {
                    var user = context.DepartmentUsers.FirstOrDefault(x => x.StudentId == student.Id);
                    if (user != null)
                    {
                        var userRole = context.DepartmentUserRoles.FirstOrDefault(x => x.UserId == user.Id && x.RoleId == role.Id);
                        if (userRole == null)
                        {
                            context.DepartmentUserRoles.Add(new DepartmentUserRole { UserId = user.Id, RoleId = role.Id });
                            context.SaveChanges();
                        }
                        else if (userRole.IsDeleted)
                        {
                            userRole.IsDeleted = false;
                            context.SaveChanges();
                        }
                        if (!user.IsDeleted)
                        {
                            user.IsDeleted = true;
                            user.DateDelete = DateTime.Now;
                            context.SaveChanges();
                        }
                    }
                }
                #endregion
                transaction.Commit();
            }
        }
    }
}