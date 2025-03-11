using System.Security.Claims;

namespace verticalSliceArchitecture.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal? principal)
        {
            string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(userId, out int parsedUserId) ?
                parsedUserId :
                throw new ApplicationException("User id is unavailable");
        }
    }
}
