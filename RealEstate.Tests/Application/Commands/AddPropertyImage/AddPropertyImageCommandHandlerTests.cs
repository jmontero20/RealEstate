using RealEstate.Application.UsecCases.PropertyImages.Commands.AddPropertyImage;
using RealEstate.Domain.Contracts;
using RealEstate.SharedKernel.Result;
using RealEstate.Tests.ObjectMothers.Commands;
using RealEstate.Tests.ObjectMothers.Entities;
using RealEstate.Domain.Entities;
using FluentAssertions;
using Moq;

namespace RealEstate.Tests.Application.Commands.AddPropertyImage;

[TestFixture]
public class AddPropertyImageCommandHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IPropertyImageRepository> _propertyImageRepositoryMock;
    private Mock<IBlobStorageService> _blobStorageServiceMock;
    private AddPropertyImageCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _propertyImageRepositoryMock = new Mock<IPropertyImageRepository>();
        _blobStorageServiceMock = new Mock<IBlobStorageService>();

        _unitOfWorkMock.Setup(x => x.PropertyImages).Returns(_propertyImageRepositoryMock.Object);

        _handler = new AddPropertyImageCommandHandler(_unitOfWorkMock.Object, _blobStorageServiceMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_ShouldAddImageSuccessfully()
    {
        // Arrange
        var command = AddPropertyImageCommandMother.Valid();
        var uploadedFileName = "uploaded_image.jpg";
        var imageUrl = "https://blob.storage.com/image.jpg";
        var createdImage = PropertyImageMother.Simple();
        createdImage.IdPropertyImage = 1;

        _blobStorageServiceMock
            .Setup(x => x.UploadImageAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Success(uploadedFileName));

        _propertyImageRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<PropertyImage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<PropertyImage>.Success(createdImage));

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _blobStorageServiceMock
            .Setup(x => x.GetImageUrlAsync(uploadedFileName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Success(imageUrl));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.ImageId.Should().Be(createdImage.IdPropertyImage);
        result.Value.PropertyId.Should().Be(command.PropertyId);
        result.Value.ImageUrl.Should().Be(imageUrl);
        result.Value.FileName.Should().Be(command.FileName);
        result.Message.Should().Be("Image added to property successfully");
    }

    [Test]
    public async Task Handle_UploadImageFails_ShouldReturnFailure()
    {
        // Arrange
        var command = AddPropertyImageCommandMother.Valid();
        var errorMessage = "Failed to upload image to blob storage";

        _blobStorageServiceMock
            .Setup(x => x.UploadImageAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Failure(errorMessage));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(errorMessage);
        result.Value.Should().BeNull();
    }

    [Test]
    public async Task Handle_CreateImageFails_ShouldReturnFailure()
    {
        // Arrange
        var command = AddPropertyImageCommandMother.Valid();
        var uploadedFileName = "uploaded_image.jpg";
        var errorMessage = "Failed to save image to database";

        _blobStorageServiceMock
            .Setup(x => x.UploadImageAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Success(uploadedFileName));

        _propertyImageRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<PropertyImage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<PropertyImage>.Failure(errorMessage));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(errorMessage);
        result.Value.Should().BeNull();
    }

    [Test]
    public async Task Handle_SaveChangesFails_ShouldReturnFailure()
    {
        // Arrange
        var command = AddPropertyImageCommandMother.Valid();
        var uploadedFileName = "uploaded_image.jpg";
        var createdImage = PropertyImageMother.Simple();
        var errorMessage = "Failed to save changes";

        _blobStorageServiceMock
            .Setup(x => x.UploadImageAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Success(uploadedFileName));

        _propertyImageRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<PropertyImage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<PropertyImage>.Success(createdImage));

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure(errorMessage));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(errorMessage);
        result.Value.Should().BeNull();
    }

    [Test]
    public async Task Handle_JpgImage_ShouldProcessCorrectly()
    {
        // Arrange
        var command = AddPropertyImageCommandMother.JpegImage();
        var uploadedFileName = "unique_image.jpg";
        var imageUrl = "https://blob.storage.com/unique_image.jpg";
        var createdImage = PropertyImageMother.JpgImage();
        createdImage.IdPropertyImage = 2;

        _blobStorageServiceMock
            .Setup(x => x.UploadImageAsync(It.IsAny<Stream>(), It.IsAny<string>(), "image/jpeg", It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Success(uploadedFileName));

        _propertyImageRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<PropertyImage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<PropertyImage>.Success(createdImage));

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _blobStorageServiceMock
            .Setup(x => x.GetImageUrlAsync(uploadedFileName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Success(imageUrl));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.FileName.Should().Be("exterior.jpg");
        result.Value.ImageUrl.Should().Be(imageUrl);
    }
}