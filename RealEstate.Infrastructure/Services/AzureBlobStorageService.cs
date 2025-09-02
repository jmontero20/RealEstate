using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using RealEstate.Domain.Comon;
using RealEstate.Domain.Contracts;
using RealEstate.SharedKernel.Result;

namespace RealEstate.Infrastructure.Services
{
    public class AzureBlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AzureBlobStorageService(BlobServiceClient blobServiceClient, string containerName)
        {
            _blobServiceClient = blobServiceClient;
            _containerName = containerName;
        }

        public async Task<Result<string>> UploadImageAsync(Stream imageStream, string fileName, string contentType, CancellationToken cancellationToken = default)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

                var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
                var blobClient = containerClient.GetBlobClient(uniqueFileName);

                imageStream.Position = 0;

                var uploadOptions = new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders { ContentType = contentType }
                };

                await blobClient.UploadAsync(imageStream, uploadOptions, cancellationToken);

                return Result<string>.Success(uniqueFileName);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Failed to upload image to Azure Blob Storage: {ex.Message}");
            }
        }

        public async Task<Result<string>> GetImageUrlAsync(string fileName, CancellationToken cancellationToken = default)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                var blobClient = containerClient.GetBlobClient(fileName);

                if (!await blobClient.ExistsAsync(cancellationToken))
                    return Result<string>.Failure($"Blob '{fileName}' not found");

                // Crear SAS que dure 7 días
                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = _containerName,
                    BlobName = fileName,
                    Resource = "b", // "b" = blob
                    ExpiresOn = DateTimeOffset.UtcNow.AddDays(7) // Expira en 7 días
                };

                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                // Necesitamos las credenciales (desde el BlobServiceClient)
                if (!_blobServiceClient.CanGenerateAccountSasUri)
                {
                    return Result<string>.Failure("The BlobServiceClient is not authorized to generate SAS tokens. Use a connection string with AccountKey.");
                }

                var sasUri = blobClient.GenerateSasUri(sasBuilder);

                return Result<string>.Success(sasUri.ToString());
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Failed to generate SAS URL: {ex.Message}");
            }
        }
    }
}
