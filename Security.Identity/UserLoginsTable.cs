using System.Collections.Generic;
using Library.Net;
using Microsoft.AspNet.Identity;

namespace Security.Identity
{
    public class UserLoginsTable
    {
        public List<UserLoginInfo> GetLogins(IdentityUser user)
        {
            var records = UserLoginERList.GetUserLoginERList(user.Id);
            var logins = new List<UserLoginInfo>();
            foreach (var record in records)
                logins.Add(new UserLoginInfo(record.LoginProvider, record.ProviderKey));
            return logins;
        }

        public void AddLogin(IdentityUser user, UserLoginInfo login)
        {
            var item = UserLoginEC.NewUserLoginEC();
            item.LoginProvider = login.LoginProvider;
            item.ProviderKey = login.ProviderKey;
            item.UserId = user.Id;
            item = item.Save();
        }

        public void RemoveLogin(IdentityUser user, UserLoginInfo login)
        {
            UserLoginEC.DeleteUserLoginEC(user.Id, login.LoginProvider, login.ProviderKey);
        }

        public string FindUserId(UserLoginInfo login)
        {
            var result = UserLoginERList.GetUserLoginERList(login.LoginProvider, login.ProviderKey);
            return result.Count > 0 ? result[0].UserId : string.Empty;
        }
    }
}