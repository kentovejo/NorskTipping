using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Security.Identity
{
    /// <summary>
    ///     Class that implements the key ASP.NET Identity user store iterfaces
    /// </summary>
    public class UserStore<TUser> : IUserStore<TUser>, IUserClaimStore<TUser>, IUserLoginStore<TUser>, IUserRoleStore<TUser>, IUserPasswordStore<TUser>, IUserLockoutStore<TUser, string>, IUserTwoFactorStore<TUser, string> where TUser : IdentityUser
    {
        private readonly RoleTable roleTable;
        private readonly UserClaimsTable userClaimsTable;
        private readonly UserLoginsTable userLoginsTable;
        private readonly UserRoleTable userRolesTable;
        private readonly UserTable<TUser> userTable;

        //public IQueryable<TUser> Users
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        /// <summary>
        ///     Default constructor that initializes a new MySQLDatabase
        ///     instance using the Default Connection string
        /// </summary>
        public UserStore()
        {
            userTable = new UserTable<TUser>();
            roleTable = new RoleTable();
            userRolesTable = new UserRoleTable();
            userClaimsTable = new UserClaimsTable();
            userLoginsTable = new UserLoginsTable();
        }

        /// <summary>
        ///     Get claims for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>
        ///     Task whose result is
        ///     <see cref="IList">
        ///         <see cref="Claim" />
        ///     </see>
        ///     for the user.
        /// </returns>
        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            var result = userClaimsTable.FindByUserId(user.Id);
            return Task.FromResult<IList<Claim>>(result.Claims.ToList());
        }

        /// <summary>
        ///     Adds a claim for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="claim">Claim to add</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task AddClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");
            var userClaim = new IdentityClaim(user.Id, claim);
            userClaimsTable.AddClaim(userClaim);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Removes a claim from a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="claim">Claim to remove</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");
            var userClaim = new IdentityClaim(user.Id, claim);
            userClaimsTable.RemoveClaim(userClaim);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Returns the Lock end date for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Task whose result is <see cref="DateTimeOffset" /> of lockout end date or default DateTimeOffset value</returns>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.LockoutEndDateUtc.HasValue ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc)) : new DateTimeOffset());
            //var lockOutDate = user.LockoutEndDateUtc.HasValue ? user.LockoutEndDateUtc.Value : new DateTimeOffset(DateTime.UtcNow.AddMinutes(-5));
            //return Task.FromResult(lockOutDate);
        }

        /// <summary>
        ///     Sets the Lockout End Date for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="lockoutEnd">The Lockout end.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.LockoutEndDateUtc = lockoutEnd.UtcDateTime;
            //userTable.Update(user);
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Increments the access failure count for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Task whose result is current count of failed access attempts</returns>
        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        ///     Resets the access failed count for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Task whose result is current count of failed access attempts</returns>
        public Task ResetAccessFailedCountAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.AccessFailedCount = 0;
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        ///     Returns the access failed count for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Task whose result is current count of failed access attempts</returns>
        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        ///     Returns true if Lockout is enabled for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Task whose result is true if lockout enabled; else task result is false</returns>
        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.LockoutEnabled);
        }

        /// <summary>
        ///     Sets Lockout enbabled for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="enabled">true if the user can be locked out; otherwise, false.</param>
        /// <returns></returns>
        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Adds login for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="login">The login information</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");
            userLoginsTable.AddLogin(user, login);
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Removes the user login
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="login">The login information</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");
            userLoginsTable.RemoveLogin(user, login);
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Returns the linked accounts for this user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>
        ///     Task whose result is a
        ///     <see cref="IList">
        ///         <see cref="UserLogin" />
        ///     </see>
        ///     for the user.
        /// </returns>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            var logins = userLoginsTable.GetLogins(user);
            return Task.FromResult<IList<UserLoginInfo>>(logins);
        }

        /// <summary>
        ///     Returns the user associated with this login
        /// </summary>
        /// <param name="login">The login information</param>
        /// <returns>Task whose result is a user</returns>
        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");
            var userId = userLoginsTable.FindUserId(login);
            var user = userTable.GetUserById(userId);
            return Task.FromResult(user);
        }

        /// <summary>
        ///     Sets the password has for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="passwordHash">Password hash</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Invalid password hash value", "passwordHash");
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Returns password hash for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Task whose result is users password hash</returns>
        public Task<string> GetPasswordHashAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            var passwordHash = userTable.GetPasswordHash(user);
            return Task.FromResult(passwordHash);
        }

        /// <summary>
        ///     Returns if user has password hash
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Task whose result is true if password hash exists; else task result is false.</returns>
        public Task<bool> HasPasswordAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            var result = !string.IsNullOrEmpty(userTable.GetPasswordHash(user));
            return Task.FromResult(result);
        }

        /// <summary>
        ///     Adds a user to a role
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="roleName">Name of role</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task AddToRoleAsync(TUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            var roleId = roleTable.GetRoleId(roleName);
            if (string.IsNullOrEmpty(roleId))
                throw new ArgumentException("Unknown role: " + roleName);
            if (!string.IsNullOrEmpty(roleId))
                userRolesTable.Insert(user, roleId);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Removes a user from a role
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="roleName">Name of the role</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            var roleId = roleTable.GetRoleId(roleName);
            if (string.IsNullOrEmpty(roleId))
                throw new ArgumentException("Unknown role: " + roleName);
            if (!string.IsNullOrEmpty(roleId))
                userRolesTable.Delete(user, roleId);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Returns roles for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>
        ///     Task whose result is
        ///     <see cref="IList">
        ///         <see cref="string" />
        ///     </see>
        ///     />
        /// </returns>
        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            IList<string> roleNames = userRolesTable.FindByUserId(user.Id).Select(role => role.Name).ToList();
            return Task.FromResult(roleNames);
        }

        /// <summary>
        ///     Returns whether a user is in a role
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="roleName">Name of the role</param>
        /// <returns>Task whose result is true if in the role;otherwise Task with result false</returns>
        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");
            var userRoles = userRolesTable.FindByUserId(user.Id);
            return Task.FromResult(userRoles.Any(x => x.Name == roleName));
        }

        /// <summary>
        ///     Releases all resources used by the current instance of the <see cref="UserStore" />
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///     Insert a new user in the users table
        /// </summary>
        /// <param name="user">User to insert</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task CreateAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            userTable.Update(user);
            return Task.FromResult(user);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Updates a user in the users table
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task UpdateAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(user.Id))
                throw new ArgumentException("Missing Id", "user");
            userTable.Update(user);
            return Task.FromResult(user);
        }

        /// <summary>
        ///     Deletes a TUser from the users table
        /// </summary>
        /// <param name="user">TUser to delete</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task DeleteAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(user.Id))
                throw new ArgumentException("Missing user Id");
            userTable.Delete(user.Id);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Find a user from the specified Id
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>Task whose result is the user; otherwise task with result null</returns>
        public Task<TUser> FindByIdAsync(string userId)
        {
            var result = userTable.GetUserById(userId);
            return Task.FromResult(result);
        }

        /// <summary>
        ///     Finds a user by the user name
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <returns>Task whose result is the user; otherwise task with result null</returns>
        public Task<TUser> FindByNameAsync(string userName)
        {
            var result = userTable.GetUserByName(userName);
            return Task.FromResult(result);
        }

        /// <summary>
        ///     Sets whether two factor authentication is enabled for the user.
        /// </summary>
        /// <param name="user">The user to set for</param>
        /// <param name="enabled">true to enable the two factor authentication; otherwise, false.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Returns whether two factor authentication is enabled for the user.
        /// </summary>
        /// <param name="user">The user to check.</param>
        /// <returns>Task whose result is true if two factor authentiation is enabled; otherwise false.</returns>
        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.TwoFactorEnabled);
        }

        /// <summary>
        ///     Sets the security stamp for the user
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="stamp">The security stamp</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Returns security stamp for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Task whose result is users security stamp</returns>
        public Task<string> GetSecurityStampAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.SecurityStamp);
        }

        /// <summary>
        ///     Sets the user email
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="email">Email address</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetEmailAsync(TUser user, string email)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.Email = email;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Returns the email address for a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Task whose result is the email address for the user</returns>
        public Task<string> GetEmailAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.Email);
        }

        /// <summary>
        ///     Returns true if the email is confirmed
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Task whose return value is true if email confirmed; otherwise task return value is false</returns>
        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>
        ///     Sets whether the the user email is confirmed
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="confirmed">true if the user e-mail is confirmed; otherwise false</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Returns user associated with the email
        /// </summary>
        /// <param name="email">The user e-mail</param>
        /// <returns>Task whose result is the user; if no user found result is null</returns>
        public Task<TUser> FindByEmailAsync(string email)
        {
            return Task.FromResult(userTable.GetUserByEmail(email));
        }

        /// <summary>
        ///     Sets the phone number associated with the user.
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="phoneNumber">The user phone number</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Gets the user phone number
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Task whose result is the users phone number</returns>
        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.PhoneNumber);
        }

        /// <summary>
        ///     Gets whether the user phone number is confirmed.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>Task whose result is true if phone number confirmed; otherwise false</returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        /// <summary>
        ///     Sets whether the user phone number is confirmed.
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="confirmed">true if the user phone number is confirmed; otherwise, false.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }
    }
}