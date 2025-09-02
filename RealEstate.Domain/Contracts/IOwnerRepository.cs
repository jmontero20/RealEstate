using RealEstate.Domain.Comon;
using RealEstate.SharedKernel.Result;

namespace RealEstate.Domain.Contracts
{
    public interface IOwnerRepository
    {
        Task<Result<bool>> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
