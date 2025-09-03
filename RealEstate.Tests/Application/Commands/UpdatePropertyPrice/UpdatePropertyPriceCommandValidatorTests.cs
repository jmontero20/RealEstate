using FluentValidation.TestHelper;
using Moq;
using RealEstate.Application.UsecCases.Property.Commands.UpdatePropertyPrice;
using RealEstate.Domain.Contracts;
using RealEstate.SharedKernel.Result;
using RealEstate.Tests.ObjectMothers.Commands;

namespace RealEstate.Tests.Application.Commands.UpdatePropertyPrice;

[TestFixture]
public class UpdatePropertyPriceCommandValidatorTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private UpdatePropertyPriceCommandValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _propertyRepositoryMock = new Mock<IPropertyRepository>();

        _unitOfWorkMock.Setup(x => x.Properties).Returns(_propertyRepositoryMock.Object);

        _validator = new UpdatePropertyPriceCommandValidator(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task Validate_ValidCommand_ShouldNotHaveErrors()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.Valid();
        SetupValidProperty(command.PropertyId);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task Validate_InvalidPropertyId_ShouldHaveError()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.WithZeroPropertyId();
        SetupValidProperty(command.PropertyId);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PropertyId)
              .WithErrorMessage("Property ID must be greater than 0");
    }

    [Test]
    public async Task Validate_NonExistentProperty_ShouldHaveError()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.WithNonExistentPropertyId();

        _propertyRepositoryMock
            .Setup(x => x.ExistsAsync(99999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(false));

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PropertyId)
              .WithErrorMessage("Property does not exist");
    }

    [Test]
    public async Task Validate_ZeroPrice_ShouldHaveError()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.WithZeroPrice();
        SetupValidProperty(command.PropertyId);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewPrice)
              .WithErrorMessage("New price must be greater than 0");
    }

    [Test]
    public async Task Validate_NegativePrice_ShouldHaveError()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.WithNegativePrice();
        SetupValidProperty(command.PropertyId);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewPrice)
              .WithErrorMessage("New price must be greater than 0");
    }

    [Test]
    public async Task Validate_ExcessivePrice_ShouldHaveError()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.WithExcessivePrice();
        SetupValidProperty(command.PropertyId);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewPrice)
              .WithErrorMessage("New price cannot exceed 999,999,999.99");
    }

    [Test]
    public async Task Validate_MaximumValidPrice_ShouldNotHaveErrors()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.WithCustomData(1, 999999999.99m);
        SetupValidProperty(command.PropertyId);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    private void SetupValidProperty(int propertyId)
    {
        _propertyRepositoryMock
            .Setup(x => x.ExistsAsync(propertyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(true));
    }
}