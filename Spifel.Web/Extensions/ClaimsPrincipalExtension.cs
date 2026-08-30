using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Spifel.Web.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetId(this ClaimsPrincipal claims)
        {
            return int.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
