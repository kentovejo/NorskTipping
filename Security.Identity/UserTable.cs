using System;
using System.Collections.Generic;
using System.Linq;
using Library.Net;

namespace Security.Identity
{
    public class UserTable<TUser> where TUser : IdentityUser
    {
        public TUser GetUserById(string id)
        {
            var userList = UserCollection.GetUserRORList().Where(a => a.Id == id);
            var list = GetUserExtracted(userList);
            return list.Count == 1 ? list[0] : null;
        }

        public TUser GetUserByName(string userName)
        {
            var userList = UserCollection.GetUserRORList().Where(a => a.UserName == userName);
            var list = GetUserExtracted(userList);
            return list.Count == 1 ? list[0] : null;
        }

        public TUser GetUserByEmail(string email)
        {
            var userList = UserCollection.GetUserRORList().Where(a => a.Email == email);
            var list = GetUserExtracted(userList);
            return list.Count == 1 ? list[0] : null;
        }

        internal void Create(TUser user)
        {
            var u = user as ApplicationUser;
            var error = new UserEdit().Create(u.UserName, u.Email, u.Comment);
            if (string.IsNullOrEmpty(error.Error))
                throw new Exception("Lagring feilet! " + error.Error);
        }

        internal void Update(TUser user)
        {
            var u = user as ApplicationUser;
            var error = new UserEdit().Update(u.UserName, u.Email, u.Comment, u.IsActive, u.PasswordHash, u.MustChangePassword, u.AccessFailedCount, u.LockoutEndDateUtc, u.SecurityStamp);
            if (!string.IsNullOrEmpty(error))
                throw new Exception("Lagring feilet! " + error);
        }

        internal string GetPasswordHash(IdentityUser user)
        {
            var u = User.GetUserEC(user.Id);
            return u.Password;
        }

        internal void Delete(string id)
        {
            var u = User.GetUserEC(id);
            u.IsActive = false;
        }

        private static List<TUser> GetUserExtracted(IEnumerable<UserReadOnly> userList)
        {
            var u = new List<TUser>();
            foreach (var row in userList)
            {
                var user = (dynamic) Activator.CreateInstance(typeof(TUser));
                user.Id = row.UserName;
                user.UserName = row.UserName;
                user.AccessFailedCount = row.FailedPasswordAttemptCount;
                user.Email = row.Email;
                user.EmailConfirmed = row.EmailConfirmed;
                user.LockoutEnabled = row.LockoutEnabled;
                user.LockoutEndDateUtc = row.LockoutEndDateUtc == DateTime.MinValue ? (DateTime?) null : row.LockoutEndDateUtc;
                user.PasswordHash = row.PasswordHash;
                user.MustChangePassword = row.MustChangePassword;
                user.PhoneNumber = row.PhoneNumber;
                user.PhoneNumberConfirmed = row.PhoneNumberConfirmed;
                user.SecurityStamp = row.SecurityStamp;
                user.TwoFactorEnabled = row.TwoFactorEnabled;
                user.UserLastLoginDateString = row.LastLoginDateString;
                user.UserFullName = row.FullName;
                user.UserFirstName = row.FirstName;
                user.UserMiddleName = row.MiddleName;
                user.UserLastName = row.LastName;
                user.UserPosition = row.Position;
                user.FkUserprofile = row.FkUserprofile;
                user.IsActive = row.IsActive;
                user.Comment = row.Comment;
                u.Add(user);
            }

            return u;
        }
    }
}