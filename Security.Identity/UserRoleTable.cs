using Library.Net;
using System;
using System.Collections.Generic;

namespace Security.Identity
{
    public class UserRoleTable
    {
        // Save user roles
        public void Insert(IdentityUser user, string roleId)
        {
            var userRoleList = UserRoleCollection.GetUserRoleERList(user.Id);
            var list = new List<object>();
            foreach (var item in userRoleList)
            {
                if (item.FkRole == roleId)
                    item.Selected = true;
                list.Add(item);
            }

            var error = new UserEdit().SaveUserRole(user.Id, list);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
        }

        public void Delete(IdentityUser user, string roleId)
        {
            var userRoleList = UserRoleCollection.GetUserRoleERList(user.Id);
            var list = new List<object>();
            foreach (var item in userRoleList)
            {
                if (item.FkRole == roleId)
                    item.Selected = false;
                list.Add(item);
            }

            var error = new UserEdit().SaveUserRole(user.Id, list);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);
        }

        public IEnumerable<IdentityRole> FindByUserId(string userId)
        {
            var userRoleList = UserRoleCollection.GetUserRoleERList(userId);
            var list = new List<IdentityRole>();
            foreach (var item in userRoleList)
                if (item.Selected)
                    list.Add(new IdentityRole {Id = item.FkRole, Name = item.Name});
            return list;
        }
    }
}