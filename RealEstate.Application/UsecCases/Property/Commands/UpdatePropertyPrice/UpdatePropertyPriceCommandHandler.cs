using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Domain.Contracts;
using RealEstate.Domain.Entities;


namespace RealEstate.Application.UsecCases.Property.Commands.UpdatePropertyPrice
{
    public class UpdatePropertyPriceCommandHandler : ICommandHandler<UpdatePropertyPriceCommand, ApplicationResponse<UpdatePropertyPriceResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePropertyPriceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationResponse<UpdatePropertyPriceResponse>> Handle(UpdatePropertyPriceCommand command, CancellationToken cancellationToken)
        {
            // Iniciar transacción para Property + PropertyTrace
            var beginResult = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (beginResult.IsFailure)
                return ApplicationResponse<UpdatePropertyPriceResponse>.FailureResponse(beginResult.Error);

            // Obtener la propiedad actual
            var propertyResult = await _unitOfWork.Properties.GetByIdAsync(command.PropertyId, cancellationToken);
            var property = propertyResult.Value;
            var oldPrice = property.Price;

            property.Price = command.NewPrice;
            property.UpdatedAt = DateTime.UtcNow;

            var updateResult = await _unitOfWork.Properties.UpdateAsync(property, cancellationToken);
            if (updateResult.IsFailure)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return ApplicationResponse<UpdatePropertyPriceResponse>.FailureResponse(updateResult.Error);
            }

            var trace = new PropertyTrace
            {
                IdProperty = command.PropertyId,
                DateSale = DateTime.UtcNow,
                Name = "Price Change",
                Value = command.NewPrice,
                Tax = command.NewPrice * 0.1m
            };

            var traceResult = await _unitOfWork.PropertyTraces.CreateAsync(trace, cancellationToken);
            if (traceResult.IsFailure)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return ApplicationResponse<UpdatePropertyPriceResponse>.FailureResponse(traceResult.Error);
            }

            var commitResult = await _unitOfWork.CommitTransactionAsync(cancellationToken);
            if (commitResult.IsFailure)
                return ApplicationResponse<UpdatePropertyPriceResponse>.FailureResponse(commitResult.Error);

            var response = new UpdatePropertyPriceResponse
            {
                PropertyId = command.PropertyId,
                OldPrice = oldPrice,
                NewPrice = command.NewPrice,
                UpdatedAt = DateTime.UtcNow
            };

            return ApplicationResponse<UpdatePropertyPriceResponse>.SuccessResponse(response, "Property price updated successfully");
        }
    }
}
