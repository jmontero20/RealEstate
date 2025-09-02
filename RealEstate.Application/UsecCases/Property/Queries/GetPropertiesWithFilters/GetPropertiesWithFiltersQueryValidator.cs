using FluentValidation;

namespace RealEstate.Application.UsecCases.Property.Queries.GetPropertiesWithFilters
{
    public class GetPropertiesWithFiltersQueryValidator : AbstractValidator<GetPropertiesWithFiltersQuery>
    {
        public GetPropertiesWithFiltersQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0")
                .LessThanOrEqualTo(100)
                .WithMessage("Page size cannot exceed 100");

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinPrice.HasValue)
                .WithMessage("Minimum price must be greater than or equal to 0");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaxPrice.HasValue)
                .WithMessage("Maximum price must be greater than or equal to 0");

            RuleFor(x => x)
                .Must(HaveValidPriceRange)
                .When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue)
                .WithMessage("Maximum price must be greater than or equal to minimum price");

            RuleFor(x => x.MinYear)
                .GreaterThan(1800)
                .When(x => x.MinYear.HasValue)
                .WithMessage("Minimum year must be greater than 1800");

            RuleFor(x => x.MaxYear)
                .LessThanOrEqualTo(DateTime.Now.Year)
                .When(x => x.MaxYear.HasValue)
                .WithMessage("Maximum year cannot be in the future");

            RuleFor(x => x)
                .Must(HaveValidYearRange)
                .When(x => x.MinYear.HasValue && x.MaxYear.HasValue)
                .WithMessage("Maximum year must be greater than or equal to minimum year");

            RuleFor(x => x.OwnerId)
                .GreaterThan(0)
                .When(x => x.OwnerId.HasValue)
                .WithMessage("Owner ID must be greater than 0");

            RuleFor(x => x.Name)
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.Name))
                .WithMessage("Name filter cannot exceed 200 characters");

            RuleFor(x => x.Address)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Address))
                .WithMessage("Address filter cannot exceed 500 characters");
        }

        private bool HaveValidPriceRange(GetPropertiesWithFiltersQuery query)
        {
            return !query.MinPrice.HasValue || !query.MaxPrice.HasValue || query.MaxPrice >= query.MinPrice;
        }

        private bool HaveValidYearRange(GetPropertiesWithFiltersQuery query)
        {
            return !query.MinYear.HasValue || !query.MaxYear.HasValue || query.MaxYear >= query.MinYear;
        }
    }
}
