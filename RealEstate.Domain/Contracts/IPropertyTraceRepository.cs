using RealEstate.Domain.Comon;
using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Contracts
{
    public interface IPropertyTraceRepository
    {
        Task<Result<PropertyTrace>> CreateAsync(PropertyTrace trace, CancellationToken cancellationToken = default);
    }
}
