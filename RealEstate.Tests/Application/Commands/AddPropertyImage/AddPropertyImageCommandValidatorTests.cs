using FluentValidation.TestHelper;
using Moq;
using RealEstate.Application.UsecCases.PropertyImages.Commands.AddPropertyImage;
using RealEstate.Domain.Contracts;
using RealEstate.SharedKernel.Result;
using RealEstate.Tests.ObjectMothers.Commands;

namespace RealEstate.Tests.Application.Commands.AddPropertyImage;

[TestFixture]
public class AddPropertyImageCommandValidatorTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private AddPropertyImageCommandValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _propertyRepositoryMock = new Mock<IPropertyRepository>();

        _unitOfWorkMock.Setup(x => x.Properties).Returns(_propertyRepositoryMock.Object);

        _validator = new AddPropertyImageCommandValidator(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task Validate_ValidCommand_ShouldNotHaveErrors()
    {
        // Arrange
        var command = AddPropertyImageCommandMother.Valid();
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
        var command = AddPropertyImageCommandMother.WithZeroPropertyId();
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
        var command = AddPropertyImageCommandMother.WithNonExistentPropertyId();

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
    public async Task Validate_EmptyFileName_ShouldHaveError()
    {
        // Arrange
        var command = AddPropertyImageCommandMother.WithEmptyFileName();
        SetupValidProperty(command.PropertyId);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FileName)
              .WithErrorMessage("File name is required");
    }

    [Test]
    public async Task Validate_InvalidFileExtension_ShouldHaveError()
    {
        // Arrange
        var command = AddPropertyImageCommandMother.WithInvalidFileExtension();
        SetupValidProperty(command.PropertyId);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FileName)
              .WithErrorMessage("File must have a valid extension (.jpg, .jpeg, .png, .gif, .webp)");
    }

    [Test]
    public async Task Validate_InvalidContentType_ShouldHaveError()
    {
        // Arrange
        var command = AddPropertyImageCommandMother.WithInvalidContentType();
        SetupValidProperty(command.PropertyId);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ContentType)
              .WithErrorMessage("Content type must be one of: image/jpeg, image/jpg, image/png, image/gif, image/webp");
    }

    [Test]
    public async Task Validate_OversizedImage_ShouldHaveError()
    {
        // Arrange
        var command = AddPropertyImageCommandMother.WithOversizedFile();
        SetupValidProperty(command.PropertyId);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ImageStream)
              .WithErrorMessage("File size cannot exceed 5MB");
    }

    [Test]
    public async Task Validate_EmptyImageStream_ShouldHaveError()
    {
        // Arrange
        var emptyStream = new MemoryStream();
        var command = new AddPropertyImageCommand(1, emptyStream, "test.jpg", "image/jpeg");
        SetupValidProperty(command.PropertyId);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ImageStream)
              .WithErrorMessage("Image file cannot be empty");
    }

    private void SetupValidProperty(int propertyId)
    {
        _propertyRepositoryMock
            .Setup(x => x.ExistsAsync(propertyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<bool>.Success(true));
    }
}