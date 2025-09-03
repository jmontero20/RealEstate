using FluentValidation.TestHelper;
using Moq;
using RealEstate.Application.UsecCases.Property.Commands.CreateProperty;
using RealEstate.Domain.Contracts;
using RealEstate.SharedKernel.Result;
using RealEstate.Tests.ObjectMothers.Commands;

namespace RealEstate.Tests.Application.Commands.CreateProperty;

[TestFixture]
public class CreatePropertyCommandValidatorTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private Mock<IOwnerRepository> _ownerRepositoryMock;
    private CreatePropertyCommandValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _ownerRepositoryMock = new Mock<IOwnerRepository>();

        _unitOfWorkMock.Setup(x => x.Properties).Returns(_propertyRepositoryMock.Object);
        _unitOfWorkMock.Setup(x => x.Owners).Returns(_ownerRepositoryMock.Object);

        _validator = new CreatePropertyCommandValidator(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task Validate_ValidCommand_ShouldNotHaveErrors()
    {
        // Arrange
        var command = CreatePropertyCommandMother.Valid();
        SetupValidOwnerAndUniqueCode(command);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public async Task Validate_EmptyName_ShouldHaveError()
    {
        // Arrange
        var command = CreatePropertyCommandMother.WithEmptyName();
        SetupValidOwnerAndUniqueCode(command);

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
        var command = CreatePropertyCommandMother.WithZeroPrice();
        SetupValidOwnerAndUniqueCode(command);

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
        var command = CreatePropertyCommandMother.WithFutureYear();
        SetupValidOwnerAndUniqueCode(command);

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
        var command = CreatePropertyCommandMother.WithDuplicateCodeInternal();
        SetupValidOwner(command.OwnerId);

        _propertyRepositoryMock
            .Setup(x => x.CodeInternalExistsAsync(It.IsAny<string>(), null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(true));

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CodeInternal)
              .WithErrorMessage("Internal code already exists");
    }

    [Test]
    public async Task Validate_NonExistentOwner_ShouldHaveError()
    {
        // Arrange
        var command = CreatePropertyCommandMother.WithNonExistentOwnerId();
        SetupUniqueCode(command.CodeInternal);

        _ownerRepositoryMock
            .Setup(x => x.ExistsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(false));

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.OwnerId)
              .WithErrorMessage("Owner does not exist");
    }

    private void SetupValidOwnerAndUniqueCode(CreatePropertyCommand command)
    {
        SetupValidOwner(command.OwnerId);
        SetupUniqueCode(command.CodeInternal);
    }

    private void SetupValidOwner(int ownerId)
    {
        _ownerRepositoryMock
            .Setup(x => x.ExistsAsync(ownerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(true));
    }

    private void SetupUniqueCode(string codeInternal)
    {
        _propertyRepositoryMock
            .Setup(x => x.CodeInternalExistsAsync(codeInternal, null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(false));
    }
}