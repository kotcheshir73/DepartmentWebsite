using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models.Authentication;
using System;
using System.Collections.Generic;

namespace DatabaseContext
{
    public class DepartmentUserManager : UserManager<DepartmentUser>
    {
        public DepartmentUserManager(IUserStore<DepartmentUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<DepartmentUser> passwordHasher, IEnumerable<IUserValidator<DepartmentUser>> userValidators,
            IEnumerable<IPasswordValidator<DepartmentUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<DepartmentUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}