using FluentValidation.TestHelper;
using RealEstate.Application.UsecCases.Property.Queries.GetPropertiesWithFilters;
using RealEstate.Tests.ObjectMothers.Queries;

namespace RealEstate.Tests.Application.Queries.GetPropertiesWithFilters;

[TestFixture]
public class GetPropertiesWithFiltersQueryValidatorTests
{
    private GetPropertiesWithFiltersQueryValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new GetPropertiesWithFiltersQueryValidator();
    }

    [Test]
    public async Task Validate_ValidQuery_ShouldNotHaveErrors()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithoutFilters();

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task Validate_InvalidPageNumber_ShouldHaveError()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithInvalidPageNumber();

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PageNumber)
              .WithErrorMessage("Page number must be greater than 0");
    }

    [Test]
    public async Task Validate_InvalidPageSize_ShouldHaveError()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithInvalidPageSize();

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PageSize)
              .WithErrorMessage("Page size must be greater than 0");
    }

    [Test]
    public async Task Validate_ExcessivePageSize_ShouldHaveError()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithExcessivePageSize();

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PageSize)
              .WithErrorMessage("Page size cannot exceed 100");
    }

    [Test]
    public async Task Validate_NegativePrices_ShouldHaveError()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithNegativePrices();

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MinPrice)
              .WithErrorMessage("Minimum price must be greater than or equal to 0");
        result.ShouldHaveValidationErrorFor(x => x.MaxPrice)
              .WithErrorMessage("Maximum price must be greater than or equal to 0");
    }

    [Test]
    public async Task Validate_InvalidPriceRange_ShouldHaveError()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithInvalidPriceRange();

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x)
              .WithErrorMessage("Maximum price must be greater than or equal to minimum price");
    }

    [Test]
    public async Task Validate_InvalidYearRange_ShouldHaveError()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithInvalidYearRange();

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x)
              .WithErrorMessage("Maximum year must be greater than or equal to minimum year");
    }

    [Test]
    public async Task Validate_MinYearTooOld_ShouldHaveError()
    {
        // Arrange
        var query = new GetPropertiesWithFiltersQuery(MinYear: 1800);

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MinYear)
              .WithErrorMessage("Minimum year must be greater than 1800");
    }

    [Test]
    public async Task Validate_MaxYearInFuture_ShouldHaveError()
    {
        // Arrange
        var futureYear = DateTime.Now.Year + 1;
        var query = new GetPropertiesWithFiltersQuery(MaxYear: futureYear);

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MaxYear)
              .WithErrorMessage("Maximum year cannot be in the future");
    }

    [Test]
    public async Task Validate_InvalidOwnerId_ShouldHaveError()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithInvalidOwnerId();

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.OwnerId)
              .WithErrorMessage("Owner ID must be greater than 0");
    }

    [Test]
    public async Task Validate_LongStrings_ShouldHaveError()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithLongStrings();

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage("Name filter cannot exceed 200 characters");
        result.ShouldHaveValidationErrorFor(x => x.Address)
              .WithErrorMessage("Address filter cannot exceed 500 characters");
    }

    [Test]
    public async Task Validate_ValidPriceRange_ShouldNotHaveErrors()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.ByPriceRange(100000m, 500000m);

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task Validate_ValidYearRange_ShouldNotHaveErrors()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.ByYearRange(2000, 2020);

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task Validate_MaximumPageSize_ShouldNotHaveErrors()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.LargePage();

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task Validate_BoundaryValues_ShouldNotHaveErrors()
    {
        // Arrange
        var currentYear = DateTime.Now.Year;
        var query = new GetPropertiesWithFiltersQuery(
            MinPrice: 0m,
            MaxPrice: 0m,
            MinYear: 1801,
            MaxYear: currentYear,
            OwnerId: 1,
            PageNumber: 1,
            PageSize: 100
        );

        // Act
        var result = await _validator.TestValidateAsync(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}