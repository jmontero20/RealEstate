using RealEstate.Domain.Comon;
using RealEstate.SharedKernel.Result;

namespace RealEstate.Domain.Contracts
{
    public interface IBlobStorageService
    {
        // Para: Add Image from property
        Task<Result<string>> UploadImageAsync(Stream imageStream, string fileName, string contentType, CancellationToken cancellationToken = default);

        // Para: List property with filters (mostrar URLs de imágenes)
        Task<Result<string>> GetImageUrlAsync(string fileName, CancellationToken cancellationToken = default);
    }
}
