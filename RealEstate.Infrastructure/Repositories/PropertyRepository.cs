using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Comon;
using RealEstate.Domain.Contracts;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;


namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly RealEstateDbContext _context;

        public PropertyRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Property>>> GetWithFiltersAsync(PropertyFilters filters, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.Properties
                    .Include(p => p.Owner)
                    .Include(p => p.PropertyImages.Where(pi => pi.Enabled))
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(filters.Name))
                    query = query.Where(p => p.Name.Contains(filters.Name));

                if (!string.IsNullOrWhiteSpace(filters.Address))
                    query = query.Where(p => p.Address.Contains(filters.Address));

                if (filters.MinPrice.HasValue)
                    query = query.Where(p => p.Price >= filters.MinPrice.Value);

                if (filters.MaxPrice.HasValue)
                    query = query.Where(p => p.Price <= filters.MaxPrice.Value);

                if (filters.MinYear.HasValue)
                    query = query.Where(p => p.Year >= filters.MinYear.Value);

                if (filters.MaxYear.HasValue)
                    query = query.Where(p => p.Year <= filters.MaxYear.Value);

                if (filters.OwnerId.HasValue)
                    query = query.Where(p => p.IdOwner == filters.OwnerId.Value);

                var properties = await query
                    .OrderBy(p => p.Name)
                    .Skip((filters.PageNumber - 1) * filters.PageSize)
                    .Take(filters.PageSize)
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<Property>>.Success(properties);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Property>>.Failure($"Error retrieving properties: {ex.Message}");
            }
        }

        public async Task<Result<Property>> CreateAsync(Property property, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Properties.AddAsync(property);
                return Result<Property>.Success(property);
            }
            catch (Exception ex)
            {
                return Result<Property>.Failure($"Error creating property: {ex.Message}");
            }
        }

        public async Task<Result<Property>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var property = await _context.Properties
                    .Include(p => p.Owner)
                    .Include(p => p.PropertyImages.Where(pi => pi.Enabled))
                    .Include(p => p.PropertyTraces)
                    .FirstOrDefaultAsync(p => p.IdProperty == id, cancellationToken);

                if (property == null)
                    return Result<Property>.Failure($"Property with ID {id} was not found");

                return Result<Property>.Success(property);
            }
            catch (Exception ex)
            {
                return Result<Property>.Failure($"Error retrieving property: {ex.Message}");
            }
        }

        public async Task<Result> UpdateAsync(Property property, CancellationToken cancellationToken = default)
        {
            try
            {
                property.UpdatedAt = DateTime.UtcNow;
                _context.Properties.Update(property);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error updating property: {ex.Message}");
            }
        }

        public async Task<Result<bool>> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var exists = await _context.Properties
                    .AnyAsync(p => p.IdProperty == id, cancellationToken);
                return Result<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error checking property existence: {ex.Message}");
            }
        }

        public async Task<Result<bool>> CodeInternalExistsAsync(string codeInternal, int? excludePropertyId = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.Properties.Where(p => p.CodeInternal == codeInternal);

                if (excludePropertyId.HasValue)
                    query = query.Where(p => p.IdProperty != excludePropertyId.Value);

                var exists = await query.AnyAsync(cancellationToken);
                return Result<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error checking code internal existence: {ex.Message}");
            }
        }

    }
}