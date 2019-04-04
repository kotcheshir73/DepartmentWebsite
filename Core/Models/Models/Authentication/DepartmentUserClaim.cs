using Microsoft.AspNetCore.Identity;
using System;

namespace Models.Authentication
{
    public class DepartmentUserClaim : IdentityUserClaim<Guid> { }
}