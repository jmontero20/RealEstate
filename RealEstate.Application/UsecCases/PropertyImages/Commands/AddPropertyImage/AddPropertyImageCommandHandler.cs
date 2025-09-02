using RealEstate.Application.Common.Interfaces;
using RealEstate.SharedKernel.Result;
using RealEstate.Domain.Contracts;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.UsecCases.PropertyImages.Commands.AddPropertyImage
{
    public class AddPropertyImageCommandHandler : ICommandHandler<AddPropertyImageCommand, Result<AddPropertyImageResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;

        public AddPropertyImageCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task<Result<AddPropertyImageResponse>> Handle(AddPropertyImageCommand command, CancellationToken cancellationToken)
        {

            var uniqueFileName = $"{Guid.NewGuid()}_{command.FileName}";
            var uploadResult = await _blobStorageService.UploadImageAsync(
                command.ImageStream, uniqueFileName, command.ContentType, cancellationToken);

            if (uploadResult.IsFailure)
                return Result<AddPropertyImageResponse>.Failure(uploadResult.Error);

            var propertyImage = new PropertyImage
            {
                IdProperty = command.PropertyId,
                File = uploadResult.Value!.ToString(), 
                Enabled = true
            };

            var createResult = await _unitOfWork.PropertyImages.CreateAsync(propertyImage, cancellationToken);
            if (createResult.IsFailure)
                return Result<AddPropertyImageResponse>.Failure(createResult.Error);

            var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (saveResult.IsFailure)
                return Result<AddPropertyImageResponse>.Failure(saveResult.Error);

            var urlResult = await _blobStorageService.GetImageUrlAsync(uploadResult.Value, cancellationToken);
            var imageUrl = urlResult.IsSuccess ? urlResult.Value : string.Empty;

            var response = new AddPropertyImageResponse
            {
                ImageId = createResult.Value!.IdPropertyImage,
                PropertyId = command.PropertyId,
                ImageUrl = imageUrl!.ToString(),
                FileName = command.FileName,
                UploadedAt = createResult.Value.CreatedAt
            };

            return Result<AddPropertyImageResponse>.Success(response, "Image added to property successfully");
        }
    }
}
