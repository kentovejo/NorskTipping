using System;
using System.Linq;
using Library.Net;

namespace Security.Identity
{
    public class RoleTable
    {
        public void Delete(string roleId)
        {
            //new RoleEdit().Delete(roleId);
        }

        public void Insert(IdentityRole role)
        {
            //new RoleEdit().Insert(role.Id, role.Name);
        }

        public string GetRoleName(string roleId)
        {
            var list = RoleERList.GetRoleERList();
            var result = list.FirstOrDefault(a => a.Name == roleId);
            return result?.Description;
        }

        public string GetRoleId(string roleName)
        {
            var list = RoleERList.GetRoleERList();
            var result = list.FirstOrDefault(a => a.Description == roleName);
            return result?.Name;
        }

        public IdentityRole GetRoleById(string roleId)
        {
            var roleName = GetRoleName(roleId);
            IdentityRole role = null;
            if (roleName != null)
            {
                role = new IdentityRole
                {
                    Id = roleId,
                    Name = roleName
                };
            }

            return role;
        }

        public IdentityRole GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            IdentityRole role = null;
            if (roleId != null)
            {
                role = new IdentityRole {Id = roleId};
                role.Name = role.Name;
            }

            return role;
        }

        public void Update(IdentityRole role)
        {
            var obj = RoleEC.GetRoleEC(role.Id);
            obj.Description = role.Name;
            obj = obj.Save();
        }
    }
}