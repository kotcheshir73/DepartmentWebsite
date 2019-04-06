using AuthenticationInterfaces.BindingModels;
using Enums;
using Models.Authentication;
using System;
using Tools;

namespace AuthenticationImplementations
{
    public static class AuthenticationModelFacotryFromBindingModel
    {
		public static DepartmentRole CreateRole(RoleSetBindingModel model, DepartmentRole entity = null)
		{
			if (entity == null)
			{
				entity = new DepartmentRole();
			}
			entity.RoleName = model.RoleName;

			return entity;
		}

		public static DepartmentAccess CreateAccess(AccessSetBindingModel model, DepartmentAccess entity = null)
		{
			if (entity == null)
			{
				entity = new DepartmentAccess();
			}
			entity.RoleId = model.RoleId;
			entity.Operation = (AccessOperation)Enum.Parse(typeof(AccessOperation), model.Operation);
			entity.AccessType = (AccessType)Enum.Parse(typeof(AccessType), model.AccessType);

			return entity;
		}

		public static DepartmentUser CreateUser(UserSetBindingModel model, DepartmentUser entity = null)
		{
			if (entity == null)
			{
				entity = new DepartmentUser
                {
					PasswordHash = DepartmentUserManager.GetPasswordHash(model.Password)
				};
			}
			entity.UserName = model.Login;
			entity.Avatar = model.Avatar;
			entity.LecturerId = model.LecturerId;
			entity.StudentId = model.StudentId;
            if (entity.IsLocked != model.IsBanned)
			{
				entity.IsLocked = model.IsBanned;
				if (model.IsBanned)
				{
					entity.DateBanned = DateTime.Now;
				}
			}
			return entity;
		}
    }
}