using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using verticalSliceArchitecture.Features.Upload.DTOs;
using verticalSliceArchitecture.Features.Upload.Services.Interface;
using verticalSliceArchitecture.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace verticalSliceArchitecture.Features.Upload.Commands
{
    public record UploadCommands : IRequest<Result>
    {
        [FromForm]
        public IFormFile File { get; set; }
    }


    public class UploadCommandHandler : IRequestHandler<UploadCommands, Result>
    {
        private readonly IUploadService _uploadService;
        private readonly IValidator<UploadCommands> _validator;

        public UploadCommandHandler(IUploadService uploadService, IValidator<UploadCommands> validator)
        {
            _uploadService = uploadService;
            _validator = validator;
        }
        public async Task<Result> Handle(UploadCommands request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Failure(Error.Failure("Validation.Error", string.Join("; ", errors)));
            }

            var uploadRequestDTO = new UploadImageRequestDTO { File = request.File };
            return await _uploadService.uploadImage(uploadRequestDTO);
        }
    }

}
