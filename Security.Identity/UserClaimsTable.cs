using System.Security.Claims;
using Library.Net;

namespace Security.Identity
{
    public class UserClaimsTable
    {
        public ClaimsIdentity FindByUserId(string userId)
        {
            var claims = new ClaimsIdentity();
            var list = UserClaimERList.GetUserClaimERList(userId);
            foreach (var item in list)
                claims.AddClaim(new IdentityClaim
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    ClaimType = item.ClaimType,
                    ClaimValue = item.ClaimValue
                });
            return claims;
        }

        public void AddClaim(IdentityClaim claim)
        {
            var ucEC = UserClaimEC.NewUserClaimEC();
            ucEC.UserId = claim.UserId;
            ucEC.ClaimValue = claim.ClaimValue;
            ucEC.ClaimType = claim.ClaimType;
            ucEC = ucEC.Save();
        }

        public void RemoveClaim(IdentityClaim claim)
        {
            UserClaimEC.DeleteUserClaimEC(claim.UserId, claim.ClaimType, claim.ClaimValue);
        }
    }
}