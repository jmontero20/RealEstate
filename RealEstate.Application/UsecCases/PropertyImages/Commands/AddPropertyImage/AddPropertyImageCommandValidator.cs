using FluentValidation;
using RealEstate.Domain.Contracts;

namespace RealEstate.Application.UsecCases.PropertyImages.Commands.AddPropertyImage
{
    public class AddPropertyImageCommandValidator : AbstractValidator<AddPropertyImageCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string[] _allowedContentTypes = { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp" };
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

        public AddPropertyImageCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.PropertyId)
                .GreaterThan(0)
                .WithMessage("Property ID must be greater than 0")
                .MustAsync(PropertyMustExist)
                .WithMessage("Property does not exist");

            RuleFor(x => x.FileName)
                .NotEmpty()
                .WithMessage("File name is required")
                .MaximumLength(255)
                .WithMessage("File name cannot exceed 255 characters")
                .Must(HaveValidFileExtension)
                .WithMessage("File must have a valid extension (.jpg, .jpeg, .png, .gif, .webp)");

            RuleFor(x => x.ContentType)
                .NotEmpty()
                .WithMessage("Content type is required")
                .Must(BeValidContentType)
                .WithMessage($"Content type must be one of: {string.Join(", ", _allowedContentTypes)}");

            RuleFor(x => x.ImageStream)
                .NotNull()
                .WithMessage("Image stream is required")
                .Must(BeValidFileSize)
                .WithMessage($"File size cannot exceed {MaxFileSize / (1024 * 1024)}MB")
                .Must(HaveContent)
                .WithMessage("Image file cannot be empty");
        }

        private async Task<bool> PropertyMustExist(int propertyId, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Properties.ExistsAsync(propertyId, cancellationToken);
            return result.IsSuccess && result.Value;
        }

        private bool BeValidContentType(string contentType)
        {
            return _allowedContentTypes.Contains(contentType.ToLower());
        }

        private bool BeValidFileSize(Stream imageStream)
        {
            return imageStream != null && imageStream.Length <= MaxFileSize;
        }

        private bool HaveContent(Stream imageStream)
        {
            return imageStream != null && imageStream.Length > 0;
        }

        private bool HaveValidFileExtension(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            var extension = Path.GetExtension(fileName).ToLower();
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            return validExtensions.Contains(extension);
        }
    }

}
