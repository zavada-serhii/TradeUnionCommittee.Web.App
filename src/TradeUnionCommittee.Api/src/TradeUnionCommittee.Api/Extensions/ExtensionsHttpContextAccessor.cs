using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace TradeUnionCommittee.Api.Extensions
{
    public static class ExtensionsHttpContextAccessor
    {
        public static string GetIp(this IHttpContextAccessor accessor)
        {
            var name = Dns.GetHostName();
            var ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
            return ip != null ? ip.ToString() : accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}