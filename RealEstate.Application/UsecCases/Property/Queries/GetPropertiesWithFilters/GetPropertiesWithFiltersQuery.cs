using RealEstate.Application.Common.Interfaces;
using RealEstate.SharedKernel.Result;


namespace RealEstate.Application.UsecCases.Property.Queries.GetPropertiesWithFilters
{
    public record GetPropertiesWithFiltersQuery(
           string? Name = null,
           string? Address = null,
           decimal? MinPrice = null,
           decimal? MaxPrice = null,
           int? MinYear = null,
           int? MaxYear = null,
           int? OwnerId = null,
           int PageNumber = 1,
           int PageSize = 10
       ) : IQuery<Result<IEnumerable<GetPropertiesWithFiltersResponse>>>;
}
