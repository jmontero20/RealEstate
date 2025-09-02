using RealEstate.Domain.Comon;
using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Contracts
{
    public interface IPropertyRepository
    {
        Task<Result<IEnumerable<Property>>> GetWithFiltersAsync(PropertyFilters filters, CancellationToken cancellationToken = default);

        Task<Result<Property>> CreateAsync(Property property, CancellationToken cancellationToken = default);

        Task<Result<Property>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(Property property, CancellationToken cancellationToken = default);

        Task<Result<bool>> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<bool>> CodeInternalExistsAsync(string codeInternal, int? excludePropertyId = null, CancellationToken cancellationToken = default);
    }
}
