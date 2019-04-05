using AuthenticationInterfaces.Interfaces;
using DatabaseContext;
using Interfaces;
using Models.Authentication;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

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
    }
}