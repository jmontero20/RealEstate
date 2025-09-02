using FluentValidation;
using RealEstate.Domain.Contracts;

namespace RealEstate.Application.UsecCases.Property.Commands.UpdateProperty
{
    public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePropertyCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Property ID must be greater than 0")
                .MustAsync(PropertyMustExist)
                .WithMessage("Property does not exist");

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
                .WithMessage("Internal code cannot exceed 50 characters");

            RuleFor(x => x)
                .MustAsync(HaveUniqueCodeInternal)
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

        private async Task<bool> PropertyMustExist(int propertyId, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Properties.ExistsAsync(propertyId, cancellationToken);
            return result.IsSuccess && result.Value;
        }

        private async Task<bool> HaveUniqueCodeInternal(UpdatePropertyCommand command, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Properties.CodeInternalExistsAsync(command.CodeInternal, command.Id, cancellationToken);
            return result.IsSuccess && !result.Value;
        }

        private async Task<bool> OwnerMustExist(int ownerId, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Owners.ExistsAsync(ownerId, cancellationToken);
            return result.IsSuccess && result.Value;
        }
    }
}
