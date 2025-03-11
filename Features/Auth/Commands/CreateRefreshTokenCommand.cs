using FluentValidation;
using MediatR;
using verticalSliceArchitecture.Features.Auth.DTOs;
using verticalSliceArchitecture.Features.Auth.Services.Interface;
using verticalSliceArchitecture.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace verticalSliceArchitecture.Features.Auth.Commands
{
    public record CreateRefreshTokenCommand(int UserId, string RefreshToken) : IRequest<Result>;

    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, Result>
    {
        private readonly IAuthService _authService;
        public CreateRefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<Result> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return Result.Failure(Shared.Error.Failure("Refresh Token", "Refresh Token is empty!!!!"));
            }
            return await _authService.generateRefreshTokenAsync(request.UserId, request.RefreshToken);
        }

    }

}
