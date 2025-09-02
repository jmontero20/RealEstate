using RealEstate.Domain.Comon;
using RealEstate.Domain.Entities;
using RealEstate.SharedKernel.Result;

namespace RealEstate.Domain.Contracts
{
    public interface IPropertyImageRepository
    {
        Task<Result<PropertyImage>> CreateAsync(PropertyImage propertyImage, CancellationToken cancellationToken = default);
    }
}
