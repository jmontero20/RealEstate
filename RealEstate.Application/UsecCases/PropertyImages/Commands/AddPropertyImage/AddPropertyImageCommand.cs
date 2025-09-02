using RealEstate.Application.Common.Interfaces;
using RealEstate.SharedKernel.Result;


namespace RealEstate.Application.UsecCases.PropertyImages.Commands.AddPropertyImage
{
    public record AddPropertyImageCommand(
        int PropertyId,
        Stream ImageStream,
        string FileName,
        string ContentType
    ) : ICommand<Result<AddPropertyImageResponse>>;
}
