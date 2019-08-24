﻿using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Security.Identity
{
    /// <inheritdoc />
    /// <summary>
    ///     Class that implements the ASP.NET Identity
    ///     IUser interface
    /// </summary>
    public class IdentityUser : IUser<string>
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        public IdentityUser()
        {
            Roles = new List<string>();
            Claims = new List<IdentityClaim>();
            Logins = new List<UserLoginInfo>();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Constructor that takes user name as argument
        /// </summary>
        /// <param name="userName">String holding name of user</param>
        public IdentityUser(string userName) : this()
        {
            Id = userName;
            UserName = userName;
        }

        public IList<string> Roles { get; }
        public IList<IdentityClaim> Claims { get; }
        public List<UserLoginInfo> Logins { get; }

        /// <summary>
        ///     Email
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        ///     True if the email is confirmed, default is false
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        ///     The salted/hashed form of the user password
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        ///     A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        ///     PhoneNumber for the user
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        ///     True if the phone number is confirmed, default is false
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        ///     Is two factor enabled for the user
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        ///     Is lockout enabled for this user
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Used to record failures for the purposes of lockout
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Users ID
        /// </summary>
        public string Id { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Users's name
        /// </summary>
        public string UserName { get; set; }
    }
}