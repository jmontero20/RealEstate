using RealEstate.Domain.Comon;

namespace RealEstate.Domain.Contracts
{
    public interface IOwnerRepository
    {
        Task<Result<bool>> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
