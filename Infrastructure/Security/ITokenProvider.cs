using verticalSliceArchitecture.Domain;

namespace verticalSliceArchitecture.Infrastructure.Security
{
    public interface ITokenProvider
    {
        string CreateToken(User user);
        string CreateRefereshToken();
    }
}
