using RealEstate.Application.UsecCases.Property.Commands.UpdatePropertyPrice;
using RealEstate.Domain.Contracts;
using RealEstate.SharedKernel.Result;
using RealEstate.Tests.ObjectMothers.Commands;
using RealEstate.Tests.ObjectMothers.Entities;
using RealEstate.Domain.Entities;
using FluentAssertions;
using Moq;

namespace RealEstate.Tests.Application.Commands.UpdatePropertyPrice;

[TestFixture]
public class UpdatePropertyPriceCommandHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private Mock<IPropertyTraceRepository> _propertyTraceRepositoryMock;
    private UpdatePropertyPriceCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _propertyTraceRepositoryMock = new Mock<IPropertyTraceRepository>();

        _unitOfWorkMock.Setup(x => x.Properties).Returns(_propertyRepositoryMock.Object);
        _unitOfWorkMock.Setup(x => x.PropertyTraces).Returns(_propertyTraceRepositoryMock.Object);

        _handler = new UpdatePropertyPriceCommandHandler(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_ShouldUpdatePriceSuccessfully()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.Valid();
        var existingProperty = PropertyMother.WithId(command.PropertyId);
        existingProperty.Price = 750000m; // Old price
        var createdTrace = PropertyTraceMother.Simple();

        SetupSuccessfulTransaction();

        _propertyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.PropertyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Success(existingProperty));

        _propertyRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _propertyTraceRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<PropertyTrace>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<PropertyTrace>.Success(createdTrace));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.PropertyId.Should().Be(command.PropertyId);
        result.Value.OldPrice.Should().Be(750000m);
        result.Value.NewPrice.Should().Be(command.NewPrice);
        result.Message.Should().Be("Property price updated successfully");

        _propertyTraceRepositoryMock.Verify(
            x => x.CreateAsync(It.Is<PropertyTrace>(pt =>
                pt.IdProperty == command.PropertyId &&
                pt.Name == "Price Change" &&
                pt.Value == command.NewPrice &&
                pt.Tax == command.NewPrice * 0.1m
            ), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task Handle_BeginTransactionFails_ShouldReturnFailure()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.Valid();
        var errorMessage = "Failed to begin transaction";

        _unitOfWorkMock
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure(errorMessage));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(errorMessage);
        result.Value.Should().BeNull();
    }

    [Test]
    public async Task Handle_UpdatePropertyFails_ShouldRollbackAndReturnFailure()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.Valid();
        var existingProperty = PropertyMother.WithId(command.PropertyId);
        var errorMessage = "Failed to update property";

        _unitOfWorkMock
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _propertyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.PropertyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Success(existingProperty));

        _propertyRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure(errorMessage));

        _unitOfWorkMock
            .Setup(x => x.RollbackTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(errorMessage);

        _unitOfWorkMock.Verify(x => x.RollbackTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_CreateTraceFails_ShouldRollbackAndReturnFailure()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.Valid();
        var existingProperty = PropertyMother.WithId(command.PropertyId);
        var errorMessage = "Failed to create property trace";

        _unitOfWorkMock
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _propertyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.PropertyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Success(existingProperty));

        _propertyRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _propertyTraceRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<PropertyTrace>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<PropertyTrace>.Failure(errorMessage));

        _unitOfWorkMock
            .Setup(x => x.RollbackTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(errorMessage);

        _unitOfWorkMock.Verify(x => x.RollbackTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_SaveChangesFails_ShouldReturnFailure()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.Valid();
        var existingProperty = PropertyMother.WithId(command.PropertyId);
        var createdTrace = PropertyTraceMother.Simple();
        var errorMessage = "Failed to save changes";

        _unitOfWorkMock
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _propertyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.PropertyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Success(existingProperty));

        _propertyRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _propertyTraceRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<PropertyTrace>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<PropertyTrace>.Success(createdTrace));

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
    public async Task Handle_CommitTransactionFails_ShouldReturnFailure()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.Valid();
        var existingProperty = PropertyMother.WithId(command.PropertyId);
        var createdTrace = PropertyTraceMother.Simple();
        var errorMessage = "Failed to commit transaction";

        _unitOfWorkMock
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _propertyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.PropertyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Success(existingProperty));

        _propertyRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _propertyTraceRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<PropertyTrace>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<PropertyTrace>.Success(createdTrace));

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _unitOfWorkMock
            .Setup(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure(errorMessage));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(errorMessage);
    }

    [Test]
    public async Task Handle_PriceChangeWithDifferentTax_ShouldCalculateCorrectly()
    {
        // Arrange
        var command = UpdatePropertyPriceCommandMother.WithNewPrice(1000000m);
        var existingProperty = PropertyMother.WithId(command.PropertyId);
        existingProperty.Price = 800000m;
        var createdTrace = PropertyTraceMother.Simple();

        SetupSuccessfulTransaction();

        _propertyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.PropertyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Success(existingProperty));

        _propertyRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _propertyTraceRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<PropertyTrace>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<PropertyTrace>.Success(createdTrace));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.OldPrice.Should().Be(800000m);
        result.Value.NewPrice.Should().Be(1000000m);

        _propertyTraceRepositoryMock.Verify(
            x => x.CreateAsync(It.Is<PropertyTrace>(pt =>
                pt.Value == 1000000m &&
                pt.Tax == 100000m // 10% of 1,000,000
            ), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private void SetupSuccessfulTransaction()
    {
        _unitOfWorkMock
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _unitOfWorkMock
            .Setup(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());
    }
}