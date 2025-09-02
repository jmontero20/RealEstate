using RealEstate.Domain.Comon;
using RealEstate.Domain.Entities;
using RealEstate.SharedKernel.Result;

namespace RealEstate.Domain.Contracts
{
    public interface IPropertyTraceRepository
    {
        Task<Result<PropertyTrace>> CreateAsync(PropertyTrace trace, CancellationToken cancellationToken = default);
    }
}
