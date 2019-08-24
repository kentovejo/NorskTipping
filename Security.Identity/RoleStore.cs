using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Security.Identity
{
    public class RoleStore : IRoleStore<IdentityRole>
    {
        private RoleTable _roleTable;

        public IQueryable<IdentityRole> Roles => throw new NotImplementedException();

        /// <summary>
        ///     Releases all resources used by the current instance of the <see cref="RoleStore" />
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///     Inserts a role.
        /// </summary>
        /// <param name="role">The Role to be inserted</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="role" /> is null</exception>
        /// <exception cref="ArgumentException">If Role.Id is null, empty or whitespace</exception>
        /// <exception cref="ArgumentException">If Role.Name is null, empty or whitespace</exception>
        /// <exception cref="ObjectDisposedException">If <see cref="RoleStore" /> has been disposed</exception>
        public Task CreateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");
            if (string.IsNullOrWhiteSpace(role.Id))
                throw new ArgumentException("Missing role Id");
            if (string.IsNullOrWhiteSpace(role.Name))
                throw new ArgumentException("Missing role Name");
            _roleTable.Insert(role);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Updates a role.
        /// </summary>
        /// <param name="role">The Role to update</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="role" /> is null</exception>
        /// <exception cref="ArgumentException">If Role.Id is null, empty or whitespace</exception>
        /// <exception cref="ArgumentException">If Role.Name is null, empty or whitespace</exception>
        /// <exception cref="ObjectDisposedException">If <see cref="RoleStore" /> has been disposed</exception>
        public Task UpdateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");
            if (string.IsNullOrWhiteSpace(role.Id))
                throw new ArgumentException("Missing role Id");
            if (string.IsNullOrWhiteSpace(role.Name))
                throw new ArgumentException("Missing role Name");
            _roleTable.Update(role);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Deletes a role.
        /// </summary>
        /// <param name="role">The role to delete</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="role" /> is null</exception>
        /// <exception cref="ArgumentException">If Role.Id is null, empty or whitespace</exception>
        /// <exception cref="ObjectDisposedException">If <see cref="RoleStore" /> has been disposed</exception>
        public Task DeleteAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");
            if (string.IsNullOrWhiteSpace(role.Id))
                throw new ArgumentException("Missing role Id");
            _roleTable.Delete(role.Id);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Finds a role by using the specified identifier.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>The task representing the asynchronous operation</returns>
        /// <exception cref="ObjectDisposedException">If <see cref="RoleStore" /> has been disposed</exception>
        public Task<IdentityRole> FindByIdAsync(string roleId)
        {
            var result = _roleTable.GetRoleById(roleId);
            return Task.FromResult(result);
        }

        /// <summary>
        ///     Finds a role by its name
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <returns>The task representing the asynchronous operation</returns>
        /// <exception cref="ObjectDisposedException">If <see cref="RoleStore" /> has been disposed</exception>
        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            var result = _roleTable.GetRoleByName(roleName);
            return Task.FromResult(result);
        }
    }
}