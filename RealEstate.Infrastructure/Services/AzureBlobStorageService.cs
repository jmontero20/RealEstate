using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using RealEstate.Domain.Comon;
using RealEstate.Domain.Contracts;
using RealEstate.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: cancellationToken);

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

                return Result<string>.Success(blobClient.Uri.ToString());
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Failed to get image URL from Azure Blob Storage: {ex.Message}");
            }
        }
    }
}
