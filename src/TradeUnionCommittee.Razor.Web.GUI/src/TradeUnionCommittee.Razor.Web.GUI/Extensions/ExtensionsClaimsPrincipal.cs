using System.Security.Claims;

namespace TradeUnionCommittee.Razor.Web.GUI.Extensions
{
    public static class ExtensionsClaimsPrincipal
    {
        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Identity.Name;
        }
    }
}
