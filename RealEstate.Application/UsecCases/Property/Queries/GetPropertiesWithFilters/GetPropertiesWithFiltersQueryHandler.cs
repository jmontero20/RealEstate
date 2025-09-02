using RealEstate.Application.Common.Interfaces;
using RealEstate.SharedKernel.Result;
using RealEstate.Domain.Comon;
using RealEstate.Domain.Contracts;

namespace RealEstate.Application.UsecCases.Property.Queries.GetPropertiesWithFilters
{
    public class GetPropertiesWithFiltersQueryHandler : IQueryHandler<GetPropertiesWithFiltersQuery, Result<IEnumerable<GetPropertiesWithFiltersResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;

        public GetPropertiesWithFiltersQueryHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<IEnumerable<GetPropertiesWithFiltersResponse>>> Handle(GetPropertiesWithFiltersQuery query, CancellationToken cancellationToken)
        {
            var filters = new PropertyFilters
            {
                Name = query.Name,
                Address = query.Address,
                MinPrice = query.MinPrice,
                MaxPrice = query.MaxPrice,
                MinYear = query.MinYear,
                MaxYear = query.MaxYear,
                OwnerId = query.OwnerId,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };

            var propertiesResult = await _unitOfWork.Properties.GetWithFiltersAsync(filters, cancellationToken);
            if (propertiesResult.IsFailure)
                return Result<IEnumerable<GetPropertiesWithFiltersResponse>>.Failure(propertiesResult.Error);

            var properties = propertiesResult.Value;
            var responseList = new List<GetPropertiesWithFiltersResponse>();

            foreach (var property in properties!)
            {
                var imageUrls = new List<string>();

                foreach (var image in property.PropertyImages.Where(pi => pi.Enabled))
                {
                    var urlResult = await _blobStorageService.GetImageUrlAsync(image.File, cancellationToken);
                    if (urlResult.IsSuccess)
                        imageUrls.Add(urlResult!.Value!);
                }

                var response = new GetPropertiesWithFiltersResponse
                {
                    PropertyId = property.IdProperty,
                    Name = property.Name,
                    Address = property.Address,
                    Price = property.Price,
                    CodeInternal = property.CodeInternal,
                    Year = property.Year,
                    OwnerName = property.Owner?.Name ?? "Unknown",
                    ImageUrls = imageUrls,
                    CreatedAt = property.CreatedAt
                };

                responseList.Add(response);
            }

            return Result<IEnumerable<GetPropertiesWithFiltersResponse>>.Success(responseList, "Properties retrieved successfully");
        }
    }
}

