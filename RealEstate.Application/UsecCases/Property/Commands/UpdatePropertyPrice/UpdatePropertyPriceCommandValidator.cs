using FluentValidation;
using RealEstate.Domain.Contracts;

namespace RealEstate.Application.UsecCases.Property.Commands.UpdatePropertyPrice
{

    public class UpdatePropertyPriceCommandValidator : AbstractValidator<UpdatePropertyPriceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePropertyPriceCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.PropertyId)
                .GreaterThan(0)
                .WithMessage("Property ID must be greater than 0")
                .MustAsync(PropertyMustExist)
                .WithMessage("Property does not exist");

            RuleFor(x => x.NewPrice)
                .GreaterThan(0)
                .WithMessage("New price must be greater than 0")
                .LessThanOrEqualTo(999999999.99m)
                .WithMessage("New price cannot exceed 999,999,999.99");
        }

        private async Task<bool> PropertyMustExist(int propertyId, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Properties.ExistsAsync(propertyId, cancellationToken);
            return result.IsSuccess && result.Value;
        }
    }

}
