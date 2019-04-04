using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models.Authentication;
using System;

namespace DatabaseContext.Stores
{
    public class DepartmentUserStore : UserStore<DepartmentUser, DepartmentRole, DatabaseContext, Guid, DepartmentUserClaim, DepartmentUserRole, DepartmentUserLogin, DepartmentUserToken, DepartmentRoleClaim>
    {
        public DepartmentUserStore(DatabaseContext context) : base(context) { }
    }
}