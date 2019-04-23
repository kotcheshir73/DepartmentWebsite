using AuthenticationModels.Models;
using AuthenticationServiceInterfaces.BindingModels;
using DepartmentModel.Enums;
using DepartmentService;
using System;

namespace AuthenticationServiceImplementations
{
    public static class AuthenticationModelFacotryFromBindingModel
    {
		public static DepartmentRole CreateRole(RoleSetBindingModel model, DepartmentRole entity = null)
		{
			if (entity == null)
			{
				entity = new DepartmentRole();
			}
			entity.Name = model.RoleName;

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
					PasswordHash = AccessCheckService.GetPasswordHash(model.Password)
				};
			}
			entity.UserName = model.Login;
			entity.Avatar = model.Avatar;
			entity.LecturerId = model.LecturerId;
			entity.StudentId = model.StudentId;
            if (entity.LockoutEnabled != model.IsBanned)
			{
				entity.LockoutEnabled = model.IsBanned;
				if (model.IsBanned)
				{
					entity.DateBanned = DateTime.Now;
				}
			}
			return entity;
		}
    }
}