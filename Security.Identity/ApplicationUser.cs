﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Security.Identity
{

    public class ApplicationUser : IdentityUser
    {
        public string Password => PasswordHash;

        public string UserLastLoginDateString { get; set; }

        public string UserFullName { get; set; }

        public string UserFirstName { get; set; }

        public string UserMiddleName { get; set; }

        public string UserLastName { get; set; }

        public string UserPosition { get; set; }

        public bool MustChangePassword { get; set; }

        public int FkUserprofile { get; set; }

        public string Comment { get; set; }

        public bool IsActive { get; set; }

        // Custom data in Cookie
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var identity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Custom claims
            identity.AddClaim(new Claim("FullName", UserFullName));
            if (!string.IsNullOrEmpty(UserFirstName))
                identity.AddClaim(new Claim("FirstName", UserFirstName));
            if (!string.IsNullOrEmpty(UserMiddleName))
                identity.AddClaim(new Claim("MiddleName", UserMiddleName));
            if (!string.IsNullOrEmpty(UserLastName))
                identity.AddClaim(new Claim("LastName", UserLastName));
            if (!string.IsNullOrEmpty(UserPosition))
                identity.AddClaim(new Claim("Position", UserPosition));
            if (!string.IsNullOrEmpty(UserLastLoginDateString))
                identity.AddClaim(new Claim("LastLoginDate", UserLastLoginDateString));
            return identity;
        }
    }
}
