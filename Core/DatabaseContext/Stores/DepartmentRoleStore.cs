using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models.Authentication;
using System;

namespace DatabaseContext.Stores
{
    public class DepartmentRoleStore : RoleStore<DepartmentRole, DatabaseContext, Guid>
    {
        public DepartmentRoleStore(DatabaseContext context) : base(context) { }
    }
}