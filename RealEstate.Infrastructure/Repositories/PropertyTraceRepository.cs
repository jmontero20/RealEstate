using RealEstate.Domain.Comon;
using RealEstate.Domain.Contracts;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly RealEstateDbContext _context;

        public PropertyTraceRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PropertyTrace>> CreateAsync(PropertyTrace trace, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.PropertyTraces.AddAsync(trace);
                return Result<PropertyTrace>.Success(trace);
            }
            catch (Exception ex)
            {
                return Result<PropertyTrace>.Failure($"Error creating property trace: {ex.Message}");
            }
        }
    }
}
