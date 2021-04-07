using Enums;
using System;
using System.Linq;
using Tools.BindingModels;

namespace DatabaseContext.Extensions
{
    public static class AccessExtension
    {
        /// <summary>
        /// Авторизация пользователя к операции
        /// </summary>
        /// <param name="coreAccess"></param>
        /// <param name="operation"></param>
        /// <param name="type"></param>
        /// <param name="entity"></param>
        public static void CheckAccess(this CoreAccessBindingModel coreAccess, AccessOperation operation, AccessType type, string entity)
        {
            if (coreAccess.SkipCheck && type == AccessType.View)
            {
                return;
            }
            using (var context = DepartmentUserManager.GetContext)
            {
                var roles = context.DepartmentUserRoles.Where(x => x.UserId == coreAccess.UserId).Select(x => x.Role).OrderByDescending(x => x.RolePriority).ToList();
                if (roles == null)
                {
                    throw new Exception($"Неверный пользователь");
                }

                var access = context.DepartmentAccesses.FirstOrDefault(a => a.Operation == operation && roles.Contains(a.Role));
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
    }
}