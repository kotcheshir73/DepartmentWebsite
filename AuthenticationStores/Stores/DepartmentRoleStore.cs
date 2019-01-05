using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DepartmentService.Context
{
    public class DepartmentRoleStore : RoleStore<DepartmentRole, Guid, DepartmentUserRole>
    {
        public DepartmentRoleStore(DepartmentDbContext context) : base(context) { }
    }
}
