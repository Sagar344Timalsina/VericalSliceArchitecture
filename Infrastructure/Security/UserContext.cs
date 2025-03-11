using verticalSliceArchitecture.Infrastructure.Extensions;
using verticalSliceArchitecture.Shared;

namespace verticalSliceArchitecture.Infrastructure.Security
{
    public sealed class UserContext:IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Id =>
            _httpContextAccessor
                .HttpContext?
                .User
                .GetUserId() ??
            throw new ApplicationException("User context is unavailable");

    }
}
