using RealEstate.Application.UsecCases.Property.Commands.UpdateProperty;
using RealEstate.Domain.Contracts;
using RealEstate.SharedKernel.Result;
using RealEstate.Tests.ObjectMothers.Commands;
using RealEstate.Tests.ObjectMothers.Entities;
using RealEstate.Domain.Entities;
using FluentAssertions;
using Moq;

namespace RealEstate.Tests.Application.Commands.UpdateProperty;

[TestFixture]
public class UpdatePropertyCommandHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private Mock<IPropertyTraceRepository> _propertyTraceRepositoryMock;
    private UpdatePropertyCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _propertyTraceRepositoryMock = new Mock<IPropertyTraceRepository>();

        _unitOfWorkMock.Setup(x => x.Properties).Returns(_propertyRepositoryMock.Object);
        _unitOfWorkMock.Setup(x => x.PropertyTraces).Returns(_propertyTraceRepositoryMock.Object);

        _handler = new UpdatePropertyCommandHandler(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task Handle_WithPriceChange_ShouldUpdatePropertyAndCreateTrace()
    {
        // Arrange
        var command = UpdatePropertyCommandMother.Valid();
        var existingProperty = PropertyMother.WithId(command.Id);
        existingProperty.Price = 100000m; // Old price
        var owner = OwnerMother.JohnSmith();
        existingProperty.Owner = owner;
        var createdTrace = PropertyTraceMother.Simple();

        SetupSuccessfulTransaction();

        _propertyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
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
        result.Value!.PropertyId.Should().Be(command.Id);
        result.Value.PriceChanged.Should().BeTrue();
        result.Value.Price.Should().Be(command.Price);
        result.Message.Should().Be("Property updated successfully");

        _propertyTraceRepositoryMock.Verify(
            x => x.CreateAsync(It.Is<PropertyTrace>(pt =>
                pt.IdProperty == command.Id &&
                pt.Name == "Property Update - Price Change" &&
                pt.Value == command.Price &&
                pt.Tax == command.Price * 0.1m
            ), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task Handle_WithoutPriceChange_ShouldUpdatePropertyWithoutTrace()
    {
        // Arrange
        var command = UpdatePropertyCommandMother.Valid();
        var existingProperty = PropertyMother.WithId(command.Id);
        existingProperty.Price = command.Price; // Same price
        var owner = OwnerMother.SarahJohnson();
        existingProperty.Owner = owner;

        SetupSuccessfulTransaction();

        _propertyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Success(existingProperty));

        _propertyRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.PriceChanged.Should().BeFalse();

        _propertyTraceRepositoryMock.Verify(
            x => x.CreateAsync(It.IsAny<PropertyTrace>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Test]
    public async Task Handle_BeginTransactionFails_ShouldReturnFailure()
    {
        // Arrange
        var command = UpdatePropertyCommandMother.Valid();
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
        var command = UpdatePropertyCommandMother.Valid();
        var existingProperty = PropertyMother.WithId(command.Id);
        var errorMessage = "Failed to update property";

        _unitOfWorkMock
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _propertyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
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
    public async Task Handle_CommitTransactionFails_ShouldReturnFailure()
    {
        // Arrange
        var command = UpdatePropertyCommandMother.Valid();
        var existingProperty = PropertyMother.WithId(command.Id);
        existingProperty.Price = command.Price;
        var errorMessage = "Failed to commit transaction";

        _unitOfWorkMock
            .Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _propertyRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Success(existingProperty));

        _propertyRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

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