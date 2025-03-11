using FluentValidation;
using MediatR;
using verticalSliceArchitecture.Features.Auth.DTOs;
using verticalSliceArchitecture.Features.Auth.Services.Interface;
using verticalSliceArchitecture.Shared;

namespace verticalSliceArchitecture.Features.Auth.Commands
{
    public record CreateRegisterCommand(RegisterRequestDto RegisterRequestDto):IRequest<Result>;

    public class CreateRegisterCommandHandler: IRequestHandler<CreateRegisterCommand, Result>
    {
        private readonly IValidator<RegisterRequestDto> _validator;
        private readonly IAuthService _authService;
        public CreateRegisterCommandHandler(IValidator<RegisterRequestDto> validator, IAuthService authService)
        {
            _authService = authService;
            _validator = validator;
        }
        public async Task<Result> Handle(CreateRegisterCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request.RegisterRequestDto, cancellationToken);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Failure(Error.Failure("Validation.Error", string.Join("; ", errors)));
            }
            return await _authService.registerUserAsync(request.RegisterRequestDto);
        }
    }

}
