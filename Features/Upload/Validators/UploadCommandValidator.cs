using FluentValidation;
using verticalSliceArchitecture.Features.Upload.Commands;

namespace verticalSliceArchitecture.Features.Upload.Validators
{
    public class UploadCommandValidator : AbstractValidator<UploadCommands>
    {
        private readonly long _maxFileSize = 2 * 1024 * 1024; // 2MB
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public UploadCommandValidator()
        {
            RuleFor(x => x.File)
                .NotNull().WithMessage("File is required.")
                .Must(f => f.Length > 0).WithMessage("File cannot be empty.")
                .Must(f => f.Length <= _maxFileSize).WithMessage($"File size must not exceed {_maxFileSize / (1024 * 1024)}MB.")
                .Must(f => _allowedExtensions.Contains(Path.GetExtension(f.FileName).ToLower()))
                .WithMessage("Invalid File type. Allowed types: jpg, jpeg, png, gif.");
        }
    }
}
