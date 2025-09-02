using FluentValidation;
using RealEstate.Domain.Contracts;


namespace RealEstate.Application.UsecCases.Property.Commands.CreateProperty
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePropertyCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Property name is required")
                .MaximumLength(200)
                .WithMessage("Property name cannot exceed 200 characters");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Property address is required")
                .MaximumLength(500)
                .WithMessage("Property address cannot exceed 500 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Property price must be greater than 0")
                .LessThanOrEqualTo(999999999.99m)
                .WithMessage("Property price cannot exceed 999,999,999.99");

            RuleFor(x => x.CodeInternal)
                .NotEmpty()
                .WithMessage("Internal code is required")
                .MaximumLength(50)
                .WithMessage("Internal code cannot exceed 50 characters")
                .MustAsync(BeUniqueCodeInternal)
                .WithMessage("Internal code already exists");

            RuleFor(x => x.Year)
                .GreaterThan(1800)
                .WithMessage("Year must be greater than 1800")
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("Year cannot be in the future");

            RuleFor(x => x.OwnerId)
                .GreaterThan(0)
                .WithMessage("Owner ID must be greater than 0")
                .MustAsync(OwnerMustExist)
                .WithMessage("Owner does not exist");
        }

        private async Task<bool> BeUniqueCodeInternal(string codeInternal, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Properties.CodeInternalExistsAsync(codeInternal, cancellationToken: cancellationToken);
            return result.IsSuccess && !result.Value;
        }

        private async Task<bool> OwnerMustExist(int ownerId, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Owners.ExistsAsync(ownerId, cancellationToken);
            return result.IsSuccess && result.Value;
        }
    }

}
