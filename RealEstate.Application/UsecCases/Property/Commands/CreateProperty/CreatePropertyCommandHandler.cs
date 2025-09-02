using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Contracts;

namespace RealEstate.Application.UsecCases.Property.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler : ICommandHandler<CreatePropertyCommand, ApplicationResponse<CreatePropertyResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePropertyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationResponse<CreatePropertyResponse>> Handle(CreatePropertyCommand command, CancellationToken cancellationToken)
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
                return ApplicationResponse<CreatePropertyResponse>.FailureResponse(createResult.Error);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return ApplicationResponse<CreatePropertyResponse>.FailureResponse(saveResult.Error);

            // Obtener información del owner para la respuesta
            var ownerResult = await _unitOfWork.Owners.ExistsAsync(command.OwnerId, cancellationToken);
            var ownerName = ownerResult.IsSuccess ? "Owner" : "Unknown"; // Simplificado

            var response = new CreatePropertyResponse
            {
                PropertyId = createResult!.Value!.IdProperty,
                Name = createResult.Value.Name,
                CodeInternal = createResult.Value.CodeInternal,
                Price = createResult.Value.Price,
                OwnerName = ownerName,
                CreatedAt = createResult.Value.CreatedAt
            };

            return ApplicationResponse<CreatePropertyResponse>.SuccessResponse(response, "Property created successfully");
        }
    }
}
