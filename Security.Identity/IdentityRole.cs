using Microsoft.AspNet.Identity;

namespace Security.Identity
{
    /// <summary>
    ///     Class that implements the ASP.NET Identity
    ///     IRole interface
    /// </summary>
    public class IdentityRole : IRole
    {
        /// <summary>
        ///     Default constructor for Roles
        /// </summary>
        public IdentityRole()
        {
        }

        /// <summary>
        ///     Constructor that takes names as argument
        /// </summary>
        /// <param name="name"></param>
        public IdentityRole(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        ///     Constructor that takes id and name as arguments
        /// </summary>
        /// <param name="id">Role id</param>
        /// <param name="name">Role name</param>
        public IdentityRole(string id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        ///     Roles ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Roles name
        /// </summary>
        public string Name { get; set; }
    }
}