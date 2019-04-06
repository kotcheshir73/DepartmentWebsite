using AuthenticationInterfaces.Interfaces;
using Enums;
using Interfaces;
using Models.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using Tools;

namespace AuthenticationImplementations.Implementations
{
    public class AdministrationProcess : IAdministrationProcess
    {
        public ResultService ImportDataToJson(string folderName)
        {
            try
            {
                Assembly assem = typeof(Models.Base.Classroom).Assembly;

                using (var context = DepartmentUserManager.GetContext)
                {
                    Type type = context.GetType();
                    var dbsets = type.GetProperties().Where(x => x.PropertyType.FullName.StartsWith("System.Data.Entity.DbSet")).ToList();
                    MethodInfo method = GetType().GetTypeInfo().GetDeclaredMethod("SaveToFile");
                    foreach (var set in dbsets)
                    {
                        var elem = assem.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                        MethodInfo generic = method.MakeGenericMethod(elem.GetType());
                        generic.Invoke(this, new object[] { folderName });
                    }
                }
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }

            return ResultService.Success();
        }

        public ResultService ExportDataFromJson(string folderName)
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                // Отключаем отслеживание и проверку изменений
                //context.Configuration.AutoDetectChangesEnabled = false;
                //context.Configuration.ValidateOnSaveEnabled = false;

                Assembly assem = typeof(Models.Base.Classroom).Assembly;
                Type type = context.GetType();
                var dbsets = type.GetProperties().Where(x => x.PropertyType.FullName.StartsWith("System.Data.Entity.DbSet")).ToList();

                #region Формируем словарь с зависимостями между данными
                Dictionary<string, int> levelDbSets = new Dictionary<string, int>();
                StringBuilder sb = new StringBuilder();
                while (levelDbSets.Count != dbsets.Count - 1)
                {
                    foreach (var set in dbsets)
                    {
                        try
                        {
                            var elem = assem.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                            var elemType = elem.GetType();
                            if (!levelDbSets.ContainsKey(elemType.Name))
                            {
                                bool flag = true;
                                int maxLevel = 0;
                                var properties = elemType.GetProperties().Where(x => x.GetMethod.IsVirtual);
                                foreach (var prop in properties)
                                {
                                    if (prop.PropertyType.Name == "List`1")
                                    {
                                        continue;
                                    }
                                    if (!levelDbSets.ContainsKey(prop.PropertyType.Name))
                                    {
                                        flag = false;
                                        break;
                                    }
                                    else if (levelDbSets[prop.PropertyType.Name] >= maxLevel)
                                    {
                                        maxLevel = levelDbSets[prop.PropertyType.Name] + 1;
                                    }
                                }
                                if (flag)
                                {
                                    levelDbSets.Add(elemType.Name, maxLevel);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            sb.AppendLine(set.PropertyType.GenericTypeArguments[0].FullName + " не загрузилось");
                        }
                    }
                }
                #endregion

                #region Удаляем записи сначала из тех, на которые никкто не ссылается и в последнюю оередь, те, на которые все ссылаются
                var deleteOrder = levelDbSets.OrderByDescending(x => x.Value);
                MethodInfo delMethod = GetType().GetTypeInfo().GetDeclaredMethod("DeleteFromDB");
                foreach (var delElem in deleteOrder)
                {
                    var set = dbsets.FirstOrDefault(x => x.PropertyType.GenericTypeArguments[0].FullName.EndsWith(delElem.Key));
                    if (set != null)
                    {
                        var elem = assem.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                        MethodInfo generic = delMethod.MakeGenericMethod(elem.GetType());
                        generic.Invoke(this, null);
                    }
                }
                #endregion

                #region Заполняем в порядке - сначала те, у которых нет родителей, потом их потомство
                MethodInfo method = GetType().GetTypeInfo().GetDeclaredMethod("LoadFromFile");
                foreach (var delElem in levelDbSets)
                {
                    var set = dbsets.FirstOrDefault(x => x.PropertyType.GenericTypeArguments[0].FullName.EndsWith(delElem.Key));
                    if (set != null)
                    {
                        var elem = assem.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                        MethodInfo generic = method.MakeGenericMethod(elem.GetType());
                        generic.Invoke(this, new object[] { folderName });
                    }
                }
                #endregion

                //context.Configuration.AutoDetectChangesEnabled = true;
                //context.Configuration.ValidateOnSaveEnabled = true;

                return ResultService.Success();
            }
        }

        public ResultService CreateBackUp(string fileName)
        {
            //try
            //{
            //    _context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format("BACKUP DATABASE [DepartmentDatabase] to DISK=N'{0}'", fileName));
            //}
            //catch (Exception ex)
            //{
            //    return ResultService.Error(ex, ResultServiceStatusCode.Error);
            //}
            return ResultService.Success();
        }

        public ResultService RestoreBackUp(string fileName)
        {
            try
            {
                // _context.Database.Delete();
                // var masterContext = new MasterDbContext();
                // masterContext.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format("RESTORE DATABASE [DepartmentDatabase] FROM DISK='{0}'", fileName));
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
            return ResultService.Success();
        }

        public ResultService SynchronizationRolesAndAccess()
        {
            try
            {
                CheckAdministrationAccesses();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }

            return ResultService.Success();
        }

        public ResultService SynchronizationUsers()
        {
            try
            {
                CheckAdminUsers();

                CheckTeacherUsers();

                CheckStudentUsers();
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }

            return ResultService.Success();
        }

        private void SaveToFile<T>(string folderName) where T : class, new()
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                var records = context.Set<T>();
                T obj = new T();
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));

                using (FileStream fs = new FileStream(string.Format("{0}/{1}.json", folderName, obj.GetType().Name), FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, records);
                }
            }
        }

        private void DeleteFromDB<T>() where T : class, new()
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                context.Set<T>().RemoveRange(context.Set<T>());
                context.SaveChanges();
            }
        }

        private void LoadFromFile<T>(string folderName) where T : class, new()
        {
            using (var context = DepartmentUserManager.GetContext)
            {
                T obj = new T();
                if (File.Exists(string.Format("{0}/{1}.json", folderName, obj.GetType().Name)))
                {
                    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
                    using (FileStream fs = new FileStream(string.Format("{0}/{1}.json", folderName, obj.GetType().Name), FileMode.Open))
                    {
                        List<T> records = (List<T>)jsonFormatter.ReadObject(fs);
                        context.Set<T>().AddRange(records);
                        context.SaveChanges();
                    }
                }
            }
        }

        private void CheckAdministrationAccesses()
        {
            using (var context = DepartmentUserManager.GetContext)
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

        private void CheckAdminUsers()
        {
            //TODO Все админы должны быть зарегестрированными в системе пользователями с ролью - админ
        }

        private void CheckTeacherUsers()
        {
            using (var context = DepartmentUserManager.GetContext)
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

        private void CheckStudentUsers()
        {
            using (var context = DepartmentUserManager.GetContext)
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