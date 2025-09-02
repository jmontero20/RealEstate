using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Contracts;
using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.UsecCases.Property.Commands.UpdateProperty
{
    public class UpdatePropertyCommandHandler : ICommandHandler<UpdatePropertyCommand, ApplicationResponse<UpdatePropertyResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePropertyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationResponse<UpdatePropertyResponse>> Handle(UpdatePropertyCommand command, CancellationToken cancellationToken)
        {
            // Iniciar transacción por si hay cambio de precio
             var beginResult = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (beginResult.IsFailure)
                return ApplicationResponse<UpdatePropertyResponse>.FailureResponse(beginResult.Error);

            // Obtener propiedad actual
            var propertyResult = await _unitOfWork.Properties.GetByIdAsync(command.Id, cancellationToken);
            var property = propertyResult.Value;
            var oldPrice = property!.Price;

            // Actualizar campos
            property.Name = command.Name;
            property.Address = command.Address;
            property.Price = command.Price;
            property.CodeInternal = command.CodeInternal;
            property.Year = command.Year;
            property.IdOwner = command.OwnerId;
            property.UpdatedAt = DateTime.UtcNow;

            var updateResult = await _unitOfWork.Properties.UpdateAsync(property, cancellationToken);
            if (updateResult.IsFailure)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return ApplicationResponse<UpdatePropertyResponse>.FailureResponse(updateResult.Error);
            }

            bool priceChanged = oldPrice != command.Price;

            if (priceChanged)
            {
                var trace = new PropertyTrace
                {
                    IdProperty = command.Id,
                    DateSale = DateTime.UtcNow,
                    Name = "Property Update - Price Change",
                    Value = command.Price,
                    Tax = command.Price * 0.1m
                };

                var traceResult = await _unitOfWork.PropertyTraces.CreateAsync(trace, cancellationToken);
                if (traceResult.IsFailure)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return ApplicationResponse<UpdatePropertyResponse>.FailureResponse(traceResult.Error);
                }
            }

            var commitResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (commitResult.IsFailure)
                return ApplicationResponse<UpdatePropertyResponse>.FailureResponse(commitResult.Error);

            var transactionResult = await _unitOfWork.CommitTransactionAsync(cancellationToken);
            if (transactionResult.IsFailure)
            {
                return ApplicationResponse<UpdatePropertyResponse>.FailureResponse(transactionResult.Error);
            }

            var response = new UpdatePropertyResponse
            {
                PropertyId = command.Id,
                Name = command.Name,
                Address = command.Address,
                Price = command.Price,
                CodeInternal = command.CodeInternal,
                Year = command.Year,
                OwnerName = property.Owner?.Name ?? "Unknown",
                UpdatedAt = DateTime.UtcNow,
                PriceChanged = priceChanged
            };

            return ApplicationResponse<UpdatePropertyResponse>.SuccessResponse(response, "Property updated successfully");
        }
    }
}
