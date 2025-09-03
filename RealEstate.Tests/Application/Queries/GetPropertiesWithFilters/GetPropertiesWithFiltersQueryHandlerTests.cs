using RealEstate.Application.UsecCases.Property.Queries.GetPropertiesWithFilters;
using RealEstate.Domain.Contracts;
using RealEstate.SharedKernel.Result;
using RealEstate.Tests.ObjectMothers.Queries;
using RealEstate.Tests.ObjectMothers.Entities;
using RealEstate.Domain.Entities;
using FluentAssertions;
using Moq;

namespace RealEstate.Tests.Application.Queries.GetPropertiesWithFilters;

[TestFixture]
public class GetPropertiesWithFiltersQueryHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private Mock<IBlobStorageService> _blobStorageServiceMock;
    private GetPropertiesWithFiltersQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _blobStorageServiceMock = new Mock<IBlobStorageService>();

        _unitOfWorkMock.Setup(x => x.Properties).Returns(_propertyRepositoryMock.Object);

        _handler = new GetPropertiesWithFiltersQueryHandler(_unitOfWorkMock.Object, _blobStorageServiceMock.Object);
    }

    [Test]
    public async Task Handle_ValidQuery_ShouldReturnPropertiesSuccessfully()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithoutFilters();
        var properties = PropertyMother.Multiple(2);
        foreach (var property in properties)
        {
            property.Owner = OwnerMother.Simple();
            property.PropertyImages = PropertyImageMother.EnabledForProperty(property.IdProperty, 2);
        }

        _propertyRepositoryMock
            .Setup(x => x.GetWithFiltersAsync(It.IsAny<Domain.Comon.PropertyFilters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IEnumerable<Property>>.Success(properties));

        _blobStorageServiceMock
            .Setup(x => x.GetImageUrlAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Success("https://test.blob.com/image.jpg"));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Should().HaveCount(2);
        result.Message.Should().Be("Properties retrieved successfully");

        var firstProperty = result.Value.First();
        firstProperty.PropertyId.Should().Be(properties[0].IdProperty);
        firstProperty.Name.Should().Be(properties[0].Name);
        firstProperty.OwnerName.Should().Be(properties[0].Owner!.Name);
        firstProperty.ImageUrls.Should().HaveCount(2);
    }

    [Test]
    public async Task Handle_NoPropertiesFound_ShouldReturnEmptyList()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithoutFilters();
        var emptyProperties = new List<Property>();

        _propertyRepositoryMock
            .Setup(x => x.GetWithFiltersAsync(It.IsAny<Domain.Comon.PropertyFilters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IEnumerable<Property>>.Success(emptyProperties));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Should().BeEmpty();
        result.Message.Should().Be("Properties retrieved successfully");
    }

    [Test]
    public async Task Handle_GetPropertiesFails_ShouldReturnFailure()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithoutFilters();
        var errorMessage = "Database error retrieving properties";

        _propertyRepositoryMock
            .Setup(x => x.GetWithFiltersAsync(It.IsAny<Domain.Comon.PropertyFilters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IEnumerable<Property>>.Failure(errorMessage));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(errorMessage);
        result.Value.Should().BeNull();
    }

    [Test]
    public async Task Handle_PropertyWithoutImages_ShouldReturnEmptyImageUrls()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithoutFilters();
        var property = PropertyMother.Simple();
        property.Owner = OwnerMother.Simple();
        property.PropertyImages = new List<PropertyImage>(); // No images

        _propertyRepositoryMock
            .Setup(x => x.GetWithFiltersAsync(It.IsAny<Domain.Comon.PropertyFilters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IEnumerable<Property>>.Success(new[] { property }));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.First().ImageUrls.Should().BeEmpty();
    }

    [Test]
    public async Task Handle_PropertyWithDisabledImages_ShouldSkipDisabledImages()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithoutFilters();
        var property = PropertyMother.Simple();
        property.Owner = OwnerMother.Simple();

        var enabledImages = PropertyImageMother.EnabledForProperty(property.IdProperty, 1);
        var disabledImages = PropertyImageMother.DisabledForProperty(property.IdProperty, 2);
        property.PropertyImages = enabledImages.Concat(disabledImages).ToList();

        _propertyRepositoryMock
            .Setup(x => x.GetWithFiltersAsync(It.IsAny<Domain.Comon.PropertyFilters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IEnumerable<Property>>.Success(new[] { property }));

        _blobStorageServiceMock
            .Setup(x => x.GetImageUrlAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Success("https://test.blob.com/image.jpg"));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.First().ImageUrls.Should().HaveCount(1); // Only enabled images
    }

    [Test]
    public async Task Handle_BlobStorageFails_ShouldSkipFailedImages()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithoutFilters();
        var property = PropertyMother.Simple();
        property.Owner = OwnerMother.Simple();
        property.PropertyImages = PropertyImageMother.EnabledForProperty(property.IdProperty, 2);

        _propertyRepositoryMock
            .Setup(x => x.GetWithFiltersAsync(It.IsAny<Domain.Comon.PropertyFilters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IEnumerable<Property>>.Success(new[] { property }));

        _blobStorageServiceMock
            .SetupSequence(x => x.GetImageUrlAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<string>.Success("https://test.blob.com/image1.jpg"))
            .ReturnsAsync(Result<string>.Failure("Blob storage error"));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.First().ImageUrls.Should().HaveCount(1); // Only successful image
        result.Value!.First().ImageUrls.First().Should().Be("https://test.blob.com/image1.jpg");
    }

    [Test]
    public async Task Handle_PropertyWithoutOwner_ShouldSetOwnerAsUnknown()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithoutFilters();
        var property = PropertyMother.Simple();
        property.Owner = null; // No owner
        property.PropertyImages = new List<PropertyImage>();

        _propertyRepositoryMock
            .Setup(x => x.GetWithFiltersAsync(It.IsAny<Domain.Comon.PropertyFilters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IEnumerable<Property>>.Success(new[] { property }));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.First().OwnerName.Should().Be("Unknown");
    }

    [Test]
    public async Task Handle_QueryWithFilters_ShouldPassFiltersCorrectly()
    {
        // Arrange
        var query = GetPropertiesWithFiltersQueryMother.WithAllFilters();
        var properties = PropertyMother.Multiple(1);
        properties[0].Owner = OwnerMother.Simple();
        properties[0].PropertyImages = new List<PropertyImage>();

        _propertyRepositoryMock
            .Setup(x => x.GetWithFiltersAsync(It.IsAny<Domain.Comon.PropertyFilters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IEnumerable<Property>>.Success(properties));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _propertyRepositoryMock.Verify(
            x => x.GetWithFiltersAsync(It.Is<Domain.Comon.PropertyFilters>(f =>
                f.Name == query.Name &&
                f.Address == query.Address &&
                f.MinPrice == query.MinPrice &&
                f.MaxPrice == query.MaxPrice &&
                f.MinYear == query.MinYear &&
                f.MaxYear == query.MaxYear &&
                f.OwnerId == query.OwnerId &&
                f.PageNumber == query.PageNumber &&
                f.PageSize == query.PageSize
            ), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}