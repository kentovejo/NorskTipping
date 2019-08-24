using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Security.Identity
{
    /// <summary>
    /// Ref: https://github.com/aspnet/AspNetIdentity
    /// Ref: https://aspnet.codeplex.com/SourceControl/latest#Samples/Identity/SQLMembership-Identity-OWIN/SQLMembership-Identity-OWIN/IdentityModels/UserManager.cs
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IdentityFactoryOptions<ApplicationUserManager> options) : base(store)
        {
            //this.UserValidator = new UserValidator<ApplicationUser>(this)
            //{
            //    AllowOnlyAlphanumericUserNames = false, 
            //    RequireUniqueEmail = true
            //};

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
            };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(20);
            MaxFailedAccessAttemptsBeforeLockout = 3;

            //// Register two-factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            //// You can write your own provider and plug it in here.
            //this.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});
            //this.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            //{
            //    Subject = "Security Code",
            //    BodyFormat = "Your security code is {0}"
            //});
            //this.EmailService = new EmailService();
            //this.SmsService = new SmsService();
            //var dataProtectionProvider = options.DataProtectionProvider;
            //if (dataProtectionProvider != null)
            //{
            //    this.UserTokenProvider =
            //        new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            //}
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(), options);
            return manager;
        }

        public void ChangePassword(string userName)
        {
            var cUser = FindByIdAsync(userName).Result;
            var hashedNewPassword = PasswordHasher.HashPassword(userName + UserPassword.GetClearPassword());
            var store = new UserStore<ApplicationUser>();
            store.SetPasswordHashAsync(cUser, hashedNewPassword);
            cUser.MustChangePassword = true;
            store.UpdateAsync(cUser);
        }
        //{
        //    string userId, IList<string> roles)

        //public IdentityResult AddUserToRoles(
        //    var userRoleStore = (IUserRoleStore<ApplicationUser, string>)Store;
        //    var user = FindById(userId);

        //    if (user == null)
        //    {
        //        throw new InvalidOperationException("Invalid user Id");
        //    }

        //    var userRoles = userRoleStore
        //        .GetRolesAsync(user)
        //        .ConfigureAwait(false);

        //    // Add user to each role using UserRoleStore
        //    foreach (var role in roles.Where(role => !userRoles.Contains(role)))
        //    {
        //        userRoleStore.AddToRoleAsync(user, role).ConfigureAwait(false);
        //    }

        //    // Call update once when all roles are added
        //    return Update(user);
        //}

        //public IdentityResult RemoveUserFromRoles(
        //     string userId, IList<string> roles)
        //{
        //    var userRoleStore = (IUserRoleStore<ApplicationUser, string>)Store;
        //    var user = FindById(userId);

        //    if (user == null)
        //    {
        //        throw new InvalidOperationException("Invalid user Id");
        //    }

        //    var userRoles = userRoleStore
        //        .GetRolesAsync(user)
        //        .ConfigureAwait(false);

        //    // Remove user to each role using UserRoleStore
        //    foreach (var role in roles.Where(userRoles.Contains))
        //    {
        //        userRoleStore
        //            .RemoveFromRoleAsync(user, role)
        //            .ConfigureAwait(false);
        //    }

        //    // Call update once when all roles are removed
        //    return UpdateAsync(user);
        //}
    }
}