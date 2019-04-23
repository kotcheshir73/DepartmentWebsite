using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DepartmentService.Context
{
    public class DepartmentUserStore : UserStore<DepartmentUser, DepartmentRole, Guid, DepartmentUserLogin, DepartmentUserRole, DepartmentUserClaim>
    {
        public DepartmentUserStore(DepartmentDbContext context) : base(context) { }
    }
}
