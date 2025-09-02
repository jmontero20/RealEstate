using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Comon;
using RealEstate.Domain.Contracts;
using RealEstate.Infrastructure.Data;
using RealEstate.SharedKernel.Result;

namespace RealEstate.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly RealEstateDbContext _context;

        public OwnerRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var exists = await _context.Owners
                    .AnyAsync(o => o.IdOwner == id, cancellationToken);
                return Result<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error checking owner existence: {ex.Message}");
            }
        }
    }
}
