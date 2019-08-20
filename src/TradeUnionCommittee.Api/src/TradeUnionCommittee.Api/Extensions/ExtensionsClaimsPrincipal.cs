using System.Security.Claims;

namespace TradeUnionCommittee.Api.Extensions
{
    public static class ExtensionsClaimsPrincipal
    {
        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Identity.Name;
        }
    }
}
