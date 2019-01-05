using AuthenticationModels.Models;
using AuthenticationServiceInterfaces.Interfaces;
using DepartmentContext;
using DepartmentContext.Stores;
using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentModel.Models;
using ScheduleModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

namespace AuthenticationServiceImplementations.Implementations
{
    public class AdministrationProcess : IAdministrationProcess
    {
        private readonly DepartmentDbContext _context;

        private DepartmentUserManager userManager;

        public AdministrationProcess(DepartmentDbContext context)
        {
            _context = context;
            userManager = new DepartmentUserManager(new DepartmentUserStore(context));
        }

        public ResultService CheckExsistData()
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(x => x.Name == "Администратор");
                if (role == null)
                {
                    CreateAdministrationRoleAndUserWithAllAccess();
                }
                role = _context.Roles.FirstOrDefault(x => x.Name == "Преподаватель");
                if (role == null)
                {
                    CreateLecturerRolesWithAllAccess();
                }
                role = _context.Roles.FirstOrDefault(x => x.Name == "Студент");
                if (role == null)
                {
                    CreateStudentRolesWithAllAccess();
                }
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

        public ResultService ImportDataToJson(string folderName)
        {
            try
            {
                Assembly assem = typeof(BaseEntity).Assembly;
                Assembly schedule = typeof(ScheduleRecord).Assembly;
                Assembly auth = typeof(DepartmentAccess).Assembly;
                Type type = _context.GetType();
                var dbsets = type.GetProperties().Where(x => x.PropertyType.FullName.StartsWith("System.Data.Entity.DbSet")).ToList();
                MethodInfo method = GetType().GetTypeInfo().GetDeclaredMethod("SaveToFile");
                foreach (var set in dbsets)
                {
                    var elem = assem.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                    if (elem == null)
                    {
                        elem = schedule.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                    }
                    else if (elem == null)
                    {
                        elem = auth.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                    }
                    MethodInfo generic = method.MakeGenericMethod(elem.GetType());
                    generic.Invoke(this, new object[] { folderName });
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
            // Отключаем отслеживание и проверку изменений
            _context.Configuration.AutoDetectChangesEnabled = false;
            _context.Configuration.ValidateOnSaveEnabled = false;

            Assembly assem = typeof(BaseEntity).Assembly;
            Assembly schedule = typeof(ScheduleRecord).Assembly;
            Assembly auth = typeof(DepartmentAccess).Assembly;
            Type type = _context.GetType();
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
                        if(elem == null)
                        {
                            elem = schedule.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                        }
                        else if (elem == null)
                        {
                            elem = auth.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                        }
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
                    catch(Exception)
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
                    if (elem == null)
                    {
                        elem = schedule.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                    }
                    else if (elem == null)
                    {
                        elem = auth.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                    }
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
                    if (elem == null)
                    {
                        elem = schedule.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                    }
                    else if (elem == null)
                    {
                        elem = auth.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                    }
                    MethodInfo generic = method.MakeGenericMethod(elem.GetType());
                    generic.Invoke(this, new object[] { folderName });
                }
            }
            #endregion

            _context.Configuration.AutoDetectChangesEnabled = true;
            _context.Configuration.ValidateOnSaveEnabled = true;

            return ResultService.Success();
        }

        public ResultService CreateBackUp(string fileName)
        {
            try
            {
                _context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format("BACKUP DATABASE [DepartmentDatabase] to DISK=N'{0}'", fileName));
            }
            catch (Exception ex)
            {
                return ResultService.Error(ex, ResultServiceStatusCode.Error);
            }
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

        private void CreateAdministrationRoleAndUserWithAllAccess()
        {
            DepartmentRole role = null;
            DepartmentUser user = null;
            using (var transaction = _context.Database.BeginTransaction())
            {
                role = new DepartmentRole
                {
                    Name = "Администратор"
                };
                _context.Roles.Add(role);
                _context.SaveChanges();

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
                _context.Accesses.AddRange(accesses);
                _context.SaveChanges();

                var md5 = new MD5CryptoServiceProvider();
                user = new DepartmentUser
                {
                    UserName = "admin",
                    PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes("qwerty")))
                };
                _context.Users.Add(user);
                _context.SaveChanges();

                transaction.Commit();
            }

            userManager.AddToRoleAsync(user.Id, role.Name).Wait();
        }

        private void CreateLecturerRolesWithAllAccess()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                DepartmentRole role = new DepartmentRole
                {
                    Name = "Преподаватель"
                };
                _context.Roles.Add(role);
                _context.SaveChanges();

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

        private void CreateStudentRolesWithAllAccess()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                DepartmentRole role = new DepartmentRole
                {
                    Name = "Студент"
                };
                _context.Roles.Add(role);
                _context.SaveChanges();

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

        private void CheckAdministrationAccesses()
        {
            var role = _context.Roles.FirstOrDefault(x => x.Name == "Администратор");
            if (role == null)
            {
                throw new Exception("Остуствует роль \"Администратор\"");
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                List<DepartmentAccess> accesses = new List<DepartmentAccess>();
                var accessInBD = _context.Accesses.Where(x => x.RoleId == role.Id).ToList();
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
                        var access = _context.Accesses.FirstOrDefault(x => x.Operation == elem && x.RoleId == role.Id);
                        access.AccessType = AccessType.Administrator;
                        _context.SaveChanges();
                    }
                }

                _context.Accesses.AddRange(accesses);

                _context.SaveChanges();

                transaction.Commit();
            }
        }

        private void CheckAdminUsers()
        {
            //TODO Все админы должны быть зарегестрированными в системе пользователями с ролью - админ
        }

        private void CheckTeacherUsers()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var role = _context.Roles.FirstOrDefault(x => x.Name == "Преподаватель");
                if (role == null)
                {
                    throw new Exception("Отсутствует роль \"Преподаватель\"");
                }
                var md5 = new MD5CryptoServiceProvider();
                #region Действующие преподаватели
                var lecturers = _context.Lecturers.Where(x => !x.IsDeleted).ToList();
                foreach (var lecturer in lecturers)
                {
                    var user = _context.Users.FirstOrDefault(x => x.LecturerId == lecturer.Id);
                    if (user == null)
                    {
                        user = new DepartmentUser
                        {
                            UserName = lecturer.ToString(),
                            PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(lecturer.ToString()))),
                            LecturerId = lecturer.Id
                        };
                        _context.Users.Add(user);
                        _context.SaveChanges();
                        
                        userManager.AddToRoleAsync(user.Id, role.Name);
                    }
                    else
                    {
                        var userRole = userManager.GetRolesAsync(user.Id);
                        if (!userRole.Result.Contains(role.Name))
                        {
                            userManager.AddToRoleAsync(user.Id, role.Name);
                        }
                        user.LockoutEnabled = false;
                        user.DateBanned = null;
                        user.IsDeleted = false;
                        user.DateDelete = null;
                        _context.SaveChanges();
                    }
                }
                #endregion
                #region Удаленные преподаватели
                var delLecturers = _context.Lecturers.Where(x => x.IsDeleted).ToList();
                foreach (var lecturer in delLecturers)
                {
                    var user = _context.Users.FirstOrDefault(x => x.LecturerId == lecturer.Id);
                    if (user != null)
                    {
                        if (!user.IsDeleted)
                        {
                            user.IsDeleted = true;
                            user.DateDelete = DateTime.Now;
                            _context.SaveChanges();
                        }
                    }
                }
                #endregion
                transaction.Commit();
            }
        }

        private void CheckStudentUsers()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var role = _context.Roles.FirstOrDefault(x => x.Name == "Студент");
                if (role == null)
                {
                    throw new Exception("Отсутствует роль \"Студент\"");
                }
                var md5 = new MD5CryptoServiceProvider();
                #region Действующие студенты
                var students = _context.Students.Where(x => !x.IsDeleted && x.StudentState == StudentState.Учится).ToList();
                foreach (var student in students)
                {
                    var user = _context.Users.FirstOrDefault(x => x.StudentId == student.Id);
                    if (user == null)
                    {
                        user = new DepartmentUser
                        {
                            UserName = student.ToString(),
                            PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(student.ToString()))),
                            StudentId = student.Id
                        };
                        _context.Users.Add(user);
                        _context.SaveChanges();


                        userManager.AddToRoleAsync(user.Id, role.Name);
                    }
                    else
                    {
                        var userRole = userManager.GetRolesAsync(user.Id);
                        if (!userRole.Result.Contains(role.Name))
                        {
                            userManager.AddToRoleAsync(user.Id, role.Name);
                        }
                        user.LockoutEnabled = false;
                        user.DateBanned = null;
                        user.IsDeleted = false;
                        user.DateDelete = null;
                        _context.SaveChanges();
                    }
                }
                #endregion
                #region Студенты отчисленные или в академе
                var bannedStudents = _context.Students.Where(x => !x.IsDeleted && (x.StudentState == StudentState.Академ || x.StudentState == StudentState.Отчислен)).ToList();
                foreach (var student in bannedStudents)
                {
                    var user = _context.Users.FirstOrDefault(x => x.StudentId == student.Id);
                    if (user == null)
                    {
                        user = new DepartmentUser
                        {
                            UserName = student.ToString(),
                            PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(student.ToString()))),
                            StudentId = student.Id
                        };
                        _context.Users.Add(user);
                        _context.SaveChanges();
                        
                        userManager.AddToRoleAsync(user.Id, role.Name);
                    }
                    else
                    {
                        var userRole = userManager.GetRolesAsync(user.Id);
                        if (!userRole.Result.Contains(role.Name))
                        {
                            userManager.AddToRoleAsync(user.Id, role.Name);
                        }
                    }
                    if (!user.LockoutEnabled)
                    {
                        user.LockoutEnabled = false;
                        user.DateBanned = DateTime.Now;
                    }
                    user.IsDeleted = false;
                    user.DateDelete = null;
                    _context.SaveChanges();
                }
                #endregion
                #region Студенты, завершившие обучение
                var finishStudents = _context.Students.Where(x => !x.IsDeleted && x.StudentState == StudentState.Завершил).ToList();
                foreach (var student in finishStudents)
                {
                    var user = _context.Users.FirstOrDefault(x => x.StudentId == student.Id);
                    if (user == null)
                    {
                        user = new DepartmentUser
                        {
                            UserName = student.ToString(),
                            PasswordHash = Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(student.ToString()))),
                            StudentId = student.Id
                        };
                        _context.Users.Add(user);
                        _context.SaveChanges();

                        userManager.AddToRoleAsync(user.Id, role.Name);
                    }
                    else
                    {
                        var userRole = userManager.GetRolesAsync(user.Id);
                        if (!userRole.Result.Contains(role.Name))
                        {
                            userManager.AddToRoleAsync(user.Id, role.Name);
                        }
                    }
                    if (!user.IsDeleted)
                    {
                        user.IsDeleted = false;
                        user.DateDelete = null;
                        _context.SaveChanges();
                    }
                }
                #endregion
                #region Удаленные студенты
                var delStudents = _context.Students.Where(x => x.IsDeleted).ToList();
                foreach (var student in delStudents)
                {
                    var user = _context.Users.FirstOrDefault(x => x.StudentId == student.Id);
                    if (user != null)
                    {
                        if (!user.IsDeleted)
                        {
                            user.IsDeleted = true;
                            user.DateDelete = DateTime.Now;
                            _context.SaveChanges();
                        }
                    }
                }
                #endregion
                transaction.Commit();
            }
        }

        private void SaveToFile<T>(string folderName) where T : class, new()
        {
            var records = _context.Set<T>();
            T obj = new T();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));

            using (FileStream fs = new FileStream(string.Format("{0}/{1}.json", folderName, obj.GetType().Name), FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, records);
            }
        }

        private void DeleteFromDB<T>() where T : class, new()
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
            _context.SaveChanges();
        }

        private void LoadFromFile<T>(string folderName) where T : class, new()
        {
            T obj = new T();
            if (File.Exists(string.Format("{0}/{1}.json", folderName, obj.GetType().Name)))
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
                using (FileStream fs = new FileStream(string.Format("{0}/{1}.json", folderName, obj.GetType().Name), FileMode.Open))
                {
                    List<T> records = (List<T>)jsonFormatter.ReadObject(fs);
                    _context.Set<T>().AddRange(records);
                    _context.SaveChanges();
                }
            }
        }
    }
}