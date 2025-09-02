using RealEstate.Domain.Comon;
using RealEstate.Domain.Contracts;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;
using RealEstate.SharedKernel.Result;


namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly RealEstateDbContext _context;

        public PropertyImageRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PropertyImage>> CreateAsync(PropertyImage propertyImage, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.PropertyImages.AddAsync(propertyImage);
                return Result<PropertyImage>.Success(propertyImage);
            }
            catch (Exception ex)
            {
                return Result<PropertyImage>.Failure($"Error creating property image: {ex.Message}");
            }
        }
    }
}
