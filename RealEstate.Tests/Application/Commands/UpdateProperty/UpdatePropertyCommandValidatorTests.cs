using FluentValidation.TestHelper;
using Moq;
using RealEstate.Application.UsecCases.Property.Commands.UpdateProperty;
using RealEstate.Domain.Contracts;
using RealEstate.SharedKernel.Result;
using RealEstate.Tests.ObjectMothers.Commands;

namespace RealEstate.Tests.Application.Commands.UpdateProperty;

[TestFixture]
public class UpdatePropertyCommandValidatorTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private Mock<IOwnerRepository> _ownerRepositoryMock;
    private UpdatePropertyCommandValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _ownerRepositoryMock = new Mock<IOwnerRepository>();

        _unitOfWorkMock.Setup(x => x.Properties).Returns(_propertyRepositoryMock.Object);
        _unitOfWorkMock.Setup(x => x.Owners).Returns(_ownerRepositoryMock.Object);

        _validator = new UpdatePropertyCommandValidator(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task Validate_ValidCommand_ShouldNotHaveErrors()
    {
        // Arrange
        var command = UpdatePropertyCommandMother.Valid();
        SetupValidPropertyOwnerAndUniqueCode(command);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task Validate_InvalidId_ShouldHaveError()
    {
        // Arrange
        var command = UpdatePropertyCommandMother.WithZeroId();
        SetupValidPropertyOwnerAndUniqueCode(command);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
              .WithErrorMessage("Property ID must be greater than 0");
    }

    [Test]
    public async Task Validate_NonExistentProperty_ShouldHaveError()
    {
        // Arrange
        var command = UpdatePropertyCommandMother.WithNonExistentId();
        SetupValidOwnerAndUniqueCode(command);

        _propertyRepositoryMock
            .Setup(x => x.ExistsAsync(99999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(false));

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
              .WithErrorMessage("Property does not exist");
    }

    [Test]
    public async Task Validate_EmptyName_ShouldHaveError()
    {
        // Arrange
        var command = UpdatePropertyCommandMother.WithEmptyName();
        SetupValidPropertyOwnerAndUniqueCode(command);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage("Property name is required");
    }

    [Test]
    public async Task Validate_InvalidPrice_ShouldHaveError()
    {
        // Arrange
        var command = UpdatePropertyCommandMother.WithZeroPrice();
        SetupValidPropertyOwnerAndUniqueCode(command);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Price)
              .WithErrorMessage("Property price must be greater than 0");
    }

    [Test]
    public async Task Validate_FutureYear_ShouldHaveError()
    {
        // Arrange
        var command = UpdatePropertyCommandMother.WithFutureYear();
        SetupValidPropertyOwnerAndUniqueCode(command);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Year)
              .WithErrorMessage("Year cannot be in the future");
    }

    [Test]
    public async Task Validate_DuplicateCodeInternal_ShouldHaveError()
    {
        // Arrange
        var command = UpdatePropertyCommandMother.WithDuplicateCodeInternal();
        SetupValidPropertyAndOwner(command);

        _propertyRepositoryMock
            .Setup(x => x.CodeInternalExistsAsync(command.CodeInternal, command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(true));

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x)
              .WithErrorMessage("Internal code already exists");
    }

    [Test]
    public async Task Validate_NonExistentOwner_ShouldHaveError()
    {
        // Arrange
        var command = UpdatePropertyCommandMother.WithNonExistentOwnerId();
        SetupValidPropertyAndUniqueCode(command);

        _ownerRepositoryMock
            .Setup(x => x.ExistsAsync(99999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(false));

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.OwnerId)
              .WithErrorMessage("Owner does not exist");
    }

    private void SetupValidPropertyOwnerAndUniqueCode(UpdatePropertyCommand command)
    {
        SetupValidProperty(command.Id);
        SetupValidOwner(command.OwnerId);
        SetupUniqueCode(command.CodeInternal, command.Id);
    }

    private void SetupValidPropertyAndOwner(UpdatePropertyCommand command)
    {
        SetupValidProperty(command.Id);
        SetupValidOwner(command.OwnerId);
    }

    private void SetupValidPropertyAndUniqueCode(UpdatePropertyCommand command)
    {
        SetupValidProperty(command.Id);
        SetupUniqueCode(command.CodeInternal, command.Id);
    }

    private void SetupValidOwnerAndUniqueCode(UpdatePropertyCommand command)
    {
        SetupValidOwner(command.OwnerId);
        SetupUniqueCode(command.CodeInternal, command.Id);
    }

    private void SetupValidProperty(int propertyId)
    {
        _propertyRepositoryMock
            .Setup(x => x.ExistsAsync(propertyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(true));
    }

    private void SetupValidOwner(int ownerId)
    {
        _ownerRepositoryMock
            .Setup(x => x.ExistsAsync(ownerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(true));
    }

    private void SetupUniqueCode(string codeInternal, int propertyId)
    {
        _propertyRepositoryMock
            .Setup(x => x.CodeInternalExistsAsync(codeInternal, propertyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(false));
    }
}