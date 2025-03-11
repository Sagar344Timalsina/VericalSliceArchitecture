using verticalSliceArchitecture.Features.Auth.DTOs;
using verticalSliceArchitecture.Shared;

namespace verticalSliceArchitecture.Features.Auth.Services.Interface
{
    public interface IAuthService
    {
        Task<Result> loginUserAsync(LoginRequestDto loginRequestDto);
        Task<Result> registerUserAsync(RegisterRequestDto registerRequestDto);

        Task<Result> generateRefreshTokenAsync(int UserId,string RefreshToken);
    }
}
