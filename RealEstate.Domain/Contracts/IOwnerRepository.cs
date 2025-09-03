using RealEstate.Domain.Comon;
using RealEstate.Domain.Entities;
using RealEstate.SharedKernel.Result;

namespace RealEstate.Domain.Contracts
{
    public interface IOwnerRepository
    {
        Task<Result<bool>> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<Owner>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
