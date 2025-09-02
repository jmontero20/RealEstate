using RealEstate.Domain.Comon;
using RealEstate.SharedKernel.Result;

namespace RealEstate.Domain.Contracts
{
    public interface IBlobStorageService
    {
        Task<Result<string>> UploadImageAsync(Stream imageStream, string fileName, string contentType, CancellationToken cancellationToken = default);

        Task<Result<string>> GetImageUrlAsync(string fileName, CancellationToken cancellationToken = default);
    }
}
