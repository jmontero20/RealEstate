namespace RealEstate.Application.UsecCases.PropertyImages.Commands.AddPropertyImage
{
    public class AddPropertyImageResponse
    {
        public int ImageId { get; set; }
        public int PropertyId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
    }
}
