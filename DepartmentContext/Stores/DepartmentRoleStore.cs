using AuthenticationModels.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DepartmentContext.Stores
{
    public class DepartmentRoleStore : RoleStore<DepartmentRole, Guid, DepartmentUserRole>
    {
        public DepartmentRoleStore(DepartmentDbContext context) : base(context) { }
    }
}