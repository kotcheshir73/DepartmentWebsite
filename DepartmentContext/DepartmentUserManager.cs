using AuthenticationModels.Models;
using DepartmentContext.Services;
using DepartmentContext.Stores;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace DepartmentContext
{
    public class DepartmentUserManager : UserManager<DepartmentUser, Guid>
    {
        public DepartmentUserManager(IUserStore<DepartmentUser, Guid> store) : base(store) { }

        public static DepartmentUserManager Create(IdentityFactoryOptions<DepartmentUserManager> options, IOwinContext context)
        {
            var manager = new DepartmentUserManager(new DepartmentUserStore(context.Get<DepartmentDbContext>()));
            // Configure validation logic for usernames 
            manager.UserValidator = new UserValidator<DepartmentUser, Guid>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords 
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Register two factor authentication providers. This application uses Phone 
            // and Emails as a step of receiving a code for verifying the user 
            // You can write your own provider and plug in here. 
            manager.RegisterTwoFactorProvider("PhoneCode",
                new PhoneNumberTokenProvider<DepartmentUser, Guid>
                {
                    MessageFormat = "Your security code is: {0}"
                });
            manager.RegisterTwoFactorProvider("EmailCode",
                new EmailTokenProvider<DepartmentUser, Guid>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is: {0}"
                });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<DepartmentUser, Guid>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}