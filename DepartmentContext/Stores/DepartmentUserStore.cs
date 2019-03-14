using AuthenticationModels.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DepartmentContext.Stores
{
    public class DepartmentUserStore : UserStore<DepartmentUser, DepartmentRole, Guid, DepartmentUserLogin, DepartmentUserRole, DepartmentUserClaim>
    {
        public DepartmentUserStore(DepartmentDbContext context) : base(context) { }
    }
}