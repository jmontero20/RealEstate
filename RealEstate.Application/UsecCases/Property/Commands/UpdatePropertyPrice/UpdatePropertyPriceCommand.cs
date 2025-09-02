using RealEstate.Application.Common.Interfaces;
using RealEstate.SharedKernel.Result;


namespace RealEstate.Application.UsecCases.Property.Commands.UpdatePropertyPrice
{
    public record UpdatePropertyPriceCommand(
        int PropertyId,
        decimal NewPrice
    ) : ICommand<Result<UpdatePropertyPriceResponse>>;
}
