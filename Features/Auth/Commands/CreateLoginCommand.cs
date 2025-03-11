

using FluentValidation;
using MediatR;
using verticalSliceArchitecture.Features.Auth.DTOs;
using verticalSliceArchitecture.Features.Auth.Services.Interface;
using verticalSliceArchitecture.Shared;

namespace verticalSliceArchitecture.Features.Auth.Commands
{
    public record CreateLoginCommand(LoginRequestDto RequestDto):IRequest<Result>;

    public class CreateLoginCommandHandler : IRequestHandler<CreateLoginCommand, Result>
    {
        private readonly IValidator<LoginRequestDto> _validator;
        private readonly IAuthService _authService;
        public CreateLoginCommandHandler(IValidator<LoginRequestDto> validator,IAuthService authService)
        { 
            _validator = validator;
            _authService = authService;
        }
        public async Task<Result> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request.RequestDto, cancellationToken);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Failure(Error.Failure("Validation.Error", string.Join("; ", errors)));
            }
            return await _authService.loginUserAsync(request.RequestDto);

        }
    }

}
