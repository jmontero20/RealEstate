using RealEstate.Application.Common.Interfaces;
using RealEstate.Domain.Contracts;
using RealEstate.SharedKernel.Result;

namespace RealEstate.Application.UsecCases.Property.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler : ICommandHandler<CreatePropertyCommand, Result<CreatePropertyResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePropertyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreatePropertyResponse>> Handle(CreatePropertyCommand command, CancellationToken cancellationToken)
        {

            var property = new Domain.Entities.Property
            {
                Name = command.Name,
                Address = command.Address,
                Price = command.Price,
                CodeInternal = command.CodeInternal,
                Year = command.Year,
                IdOwner = command.OwnerId
            };

            var createResult = await _unitOfWork.Properties.CreateAsync(property, cancellationToken);
            if (createResult.IsFailure)
                return Result<CreatePropertyResponse>.Failure(createResult.Error);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return Result<CreatePropertyResponse>.Failure(saveResult.Error);

            // Obtener información del owner para la respuesta
            var ownerResult = await _unitOfWork.Owners.GetByIdAsync(command.OwnerId, cancellationToken);
            var ownerName = ownerResult.IsSuccess ? ownerResult.Value!.Name : "Unknown"; // Simplificado

            var response = new CreatePropertyResponse
            {
                PropertyId = createResult!.Value!.IdProperty,
                Name = createResult.Value.Name,
                CodeInternal = createResult.Value.CodeInternal,
                Price = createResult.Value.Price,
                OwnerName = ownerName,
                CreatedAt = createResult.Value.CreatedAt
            };

            return Result<CreatePropertyResponse>.Success(response, "Property created successfully");
        }
    }
}
