using RealEstate.Application.UsecCases.Property.Commands.CreateProperty;
using RealEstate.Domain.Contracts;
using RealEstate.SharedKernel.Result;
using RealEstate.Tests.ObjectMothers.Commands;
using RealEstate.Tests.ObjectMothers.Entities;
using RealEstate.Domain.Entities;
using FluentAssertions;
using Moq;

namespace RealEstate.Tests.Application.Commands.CreateProperty;

[TestFixture]
public class CreatePropertyCommandHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private Mock<IOwnerRepository> _ownerRepositoryMock;
    private CreatePropertyCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _ownerRepositoryMock = new Mock<IOwnerRepository>();

        _unitOfWorkMock.Setup(x => x.Properties).Returns(_propertyRepositoryMock.Object);
        _unitOfWorkMock.Setup(x => x.Owners).Returns(_ownerRepositoryMock.Object);

        _handler = new CreatePropertyCommandHandler(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_ShouldCreatePropertySuccessfully()
    {
        // Arrange
        var command = CreatePropertyCommandMother.Valid();
        var createdProperty = PropertyMother.Simple();
        createdProperty.IdProperty = 1;
        var owner = OwnerMother.JohnSmith();

        _propertyRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Success(createdProperty));

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _ownerRepositoryMock
            .Setup(x => x.GetByIdAsync(command.OwnerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Owner>.Success(owner));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.PropertyId.Should().Be(createdProperty.IdProperty);
        result.Value.Name.Should().Be(createdProperty.Name);
        result.Value.CodeInternal.Should().Be(createdProperty.CodeInternal);
        result.Value.Price.Should().Be(createdProperty.Price);
        result.Value.OwnerName.Should().Be(owner.Name);
        result.Value.CreatedAt.Should().Be(createdProperty.CreatedAt);
        result.Message.Should().Be("Property created successfully");
    }

    [Test]
    public async Task Handle_OwnerNotFound_ShouldSetOwnerNameAsUnknown()
    {
        // Arrange
        var command = CreatePropertyCommandMother.Valid();
        var createdProperty = PropertyMother.Simple();
        createdProperty.IdProperty = 2;

        _propertyRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Success(createdProperty));

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _ownerRepositoryMock
            .Setup(x => x.GetByIdAsync(command.OwnerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Owner>.Failure("Owner not found"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.OwnerName.Should().Be("Unknown");
        result.Value.PropertyId.Should().Be(createdProperty.IdProperty);
    }

    [Test]
    public async Task Handle_CreatePropertyFails_ShouldReturnFailure()
    {
        // Arrange
        var command = CreatePropertyCommandMother.Valid();
        var errorMessage = "Database error creating property";

        _propertyRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Failure(errorMessage));

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
        var command = CreatePropertyCommandMother.Valid();
        var createdProperty = PropertyMother.Simple();
        var errorMessage = "Failed to save to database";

        _propertyRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Success(createdProperty));

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
    public async Task Handle_LuxuryProperty_ShouldCreateWithCorrectOwnerName()
    {
        // Arrange
        var command = CreatePropertyCommandMother.LuxuryProperty();
        var createdProperty = PropertyMother.Expensive();
        createdProperty.IdProperty = 3;
        var owner = OwnerMother.MichaelBrown(); // Michael Brown para lujo

        _propertyRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<Property>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Property>.Success(createdProperty));

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        _ownerRepositoryMock
            .Setup(x => x.GetByIdAsync(command.OwnerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Owner>.Success(owner));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.Name.Should().Be(createdProperty.Name);
        result.Value.Price.Should().Be(createdProperty.Price);
        result.Value.OwnerName.Should().Be(owner.Name);
        result.Value.CodeInternal.Should().Be(createdProperty.CodeInternal);
    }
}