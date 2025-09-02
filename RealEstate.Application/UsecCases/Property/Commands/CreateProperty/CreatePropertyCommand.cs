using RealEstate.Application.Common.Interfaces;
using RealEstate.SharedKernel.Result;

namespace RealEstate.Application.UsecCases.Property.Commands.CreateProperty
{
    public record CreatePropertyCommand(
        string Name,
        string Address,
        decimal Price,
        string CodeInternal,
        int Year,
        int OwnerId
    ) : ICommand<Result<CreatePropertyResponse>>;
}
