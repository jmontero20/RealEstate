using Bogus;
using RealEstate.Domain.Entities;

namespace RealEstate.Tests.ObjectMothers.Entities;

public static class PropertyImageMother
{
    private static readonly Faker<PropertyImage> _faker = new Faker<PropertyImage>()
        .RuleFor(pi => pi.IdPropertyImage, f => f.Random.Int(1, 10000))
        .RuleFor(pi => pi.IdProperty, f => f.Random.Int(1, 1000))
        .RuleFor(pi => pi.File, f => $"{f.Random.Guid()}_{f.System.CommonFileName("jpg")}")
        .RuleFor(pi => pi.Enabled, f => true)
        .RuleFor(pi => pi.CreatedAt, f => f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now))
        .RuleFor(pi => pi.UpdatedAt, f => null)
        .RuleFor(pi => pi.IsDeleted, f => false);

    private static readonly string[] ImageExtensions = { "jpg", "jpeg", "png", "gif", "webp" };
    private static readonly string[] ImagePrefixes = { "property", "house", "apartment", "room", "exterior", "interior" };

    #region Basic Scenarios

    /// <summary>
    /// Creates a simple valid property image
    /// </summary>
    public static PropertyImage Simple()
    {
        return _faker.Generate();
    }

    /// <summary>
    /// Creates a property image for specific property
    /// </summary>
    public static PropertyImage ForProperty(int propertyId)
    {
        var image = _faker.Generate();
        image.IdProperty = propertyId;
        return image;
    }

    /// <summary>
    /// Creates a property image with property relationship
    /// </summary>
    public static PropertyImage WithProperty()
    {
        var image = _faker.Generate();
        image.Property = PropertyMother.Simple();
        image.Property.IdProperty = image.IdProperty;
        return image;
    }

    /// <summary>
    /// Creates an enabled property image
    /// </summary>
    public static PropertyImage Enabled()
    {
        var image = _faker.Generate();
        image.Enabled = true;
        return image;
    }

    /// <summary>
    /// Creates a disabled property image
    /// </summary>
    public static PropertyImage Disabled()
    {
        var image = _faker.Generate();
        image.Enabled = false;
        return image;
    }

    #endregion

    #region File Type Scenarios

    /// <summary>
    /// Creates a JPG image
    /// </summary>
    public static PropertyImage JpgImage()
    {
        var image = _faker.Generate();
        image.File = $"{Guid.NewGuid()}_property.jpg";
        return image;
    }

    /// <summary>
    /// Creates a PNG image
    /// </summary>
    public static PropertyImage PngImage()
    {
        var image = _faker.Generate();
        image.File = $"{Guid.NewGuid()}_property.png";
        return image;
    }

    /// <summary>
    /// Creates a GIF image
    /// </summary>
    public static PropertyImage GifImage()
    {
        var image = _faker.Generate();
        image.File = $"{Guid.NewGuid()}_property.gif";
        return image;
    }

    /// <summary>
    /// Creates a WebP image
    /// </summary>
    public static PropertyImage WebPImage()
    {
        var image = _faker.Generate();
        image.File = $"{Guid.NewGuid()}_property.webp";
        return image;
    }

    #endregion

    #region Image Purpose Scenarios

    /// <summary>
    /// Creates an exterior property image
    /// </summary>
    public static PropertyImage ExteriorImage()
    {
        var image = _faker.Generate();
        image.File = $"{Guid.NewGuid()}_exterior_view.jpg";
        return image;
    }

    /// <summary>
    /// Creates an interior property image
    /// </summary>
    public static PropertyImage InteriorImage()
    {
        var image = _faker.Generate();
        image.File = $"{Guid.NewGuid()}_interior_room.jpg";
        return image;
    }

    /// <summary>
    /// Creates a kitchen property image
    /// </summary>
    public static PropertyImage KitchenImage()
    {
        var image = _faker.Generate();
        image.File = $"{Guid.NewGuid()}_kitchen.jpg";
        return image;
    }

    /// <summary>
    /// Creates a bathroom property image
    /// </summary>
    public static PropertyImage BathroomImage()
    {
        var image = _faker.Generate();
        image.File = $"{Guid.NewGuid()}_bathroom.jpg";
        return image;
    }

    /// <summary>
    /// Creates a bedroom property image
    /// </summary>
    public static PropertyImage BedroomImage()
    {
        var image = _faker.Generate();
        image.File = $"{Guid.NewGuid()}_bedroom.jpg";
        return image;
    }

    #endregion

    #region Validation Scenarios

    /// <summary>
    /// Creates a property image with invalid empty file name
    /// </summary>
    public static PropertyImage WithInvalidFileName()
    {
        var image = _faker.Generate();
        image.File = string.Empty;
        return image;
    }

    /// <summary>
    /// Creates a property image with file name too long
    /// </summary>
    public static PropertyImage WithLongFileName()
    {
        var image = _faker.Generate();
        image.File = new string('a', 256) + ".jpg"; // Exceeds 255 character limit
        return image;
    }

    /// <summary>
    /// Creates a property image with invalid extension
    /// </summary>
    public static PropertyImage WithInvalidExtension()
    {
        var image = _faker.Generate();
        image.File = $"{Guid.NewGuid()}_property.txt"; // Not an image extension
        return image;
    }

    /// <summary>
    /// Creates a property image with no extension
    /// </summary>
    public static PropertyImage WithoutExtension()
    {
        var image = _faker.Generate();
        image.File = $"{Guid.NewGuid()}_property"; // No extension
        return image;
    }

    /// <summary>
    /// Creates a property image with invalid property ID
    /// </summary>
    public static PropertyImage WithInvalidPropertyId()
    {
        var image = _faker.Generate();
        image.IdProperty = -1; // Invalid ID
        return image;
    }

    #endregion

    #region State Scenarios

    /// <summary>
    /// Creates a soft-deleted property image
    /// </summary>
    public static PropertyImage Deleted()
    {
        var image = _faker.Generate();
        image.IsDeleted = true;
        return image;
    }

    /// <summary>
    /// Creates a recently updated property image
    /// </summary>
    public static PropertyImage Updated()
    {
        var image = _faker.Generate();
        image.UpdatedAt = DateTime.UtcNow;
        return image;
    }

    /// <summary>
    /// Creates a property image uploaded today
    /// </summary>
    public static PropertyImage UploadedToday()
    {
        var image = _faker.Generate();
        image.CreatedAt = DateTime.UtcNow.Date;
        return image;
    }

    /// <summary>
    /// Creates an old property image
    /// </summary>
    public static PropertyImage Old()
    {
        var image = _faker.Generate();
        image.CreatedAt = DateTime.UtcNow.AddYears(-2);
        return image;
    }

    #endregion

    #region Custom Scenarios

    /// <summary>
    /// Creates a property image with specific ID
    /// </summary>
    public static PropertyImage WithId(int id)
    {
        var image = _faker.Generate();
        image.IdPropertyImage = id;
        return image;
    }

    /// <summary>
    /// Creates a property image with specific file name
    /// </summary>
    public static PropertyImage WithFileName(string fileName)
    {
        var image = _faker.Generate();
        image.File = fileName;
        return image;
    }

    /// <summary>
    /// Creates a property image with specific enabled status
    /// </summary>
    public static PropertyImage WithEnabledStatus(bool enabled)
    {
        var image = _faker.Generate();
        image.Enabled = enabled;
        return image;
    }

    /// <summary>
    /// Creates a property image with Azure Blob Storage format
    /// </summary>
    public static PropertyImage AzureBlobFormat()
    {
        var image = _faker.Generate();
        var guid = Guid.NewGuid();
        var extension = new Faker().PickRandom(ImageExtensions);
        image.File = $"{guid}_property_image.{extension}";
        return image;
    }

    /// <summary>
    /// Creates a property image with specific creation date
    /// </summary>
    public static PropertyImage WithCreationDate(DateTime createdAt)
    {
        var image = _faker.Generate();
        image.CreatedAt = createdAt;
        return image;
    }

    #endregion

    #region Lists

    /// <summary>
    /// Creates multiple property images
    /// </summary>
    public static List<PropertyImage> Multiple(int count = 3, int? propertyId = null)
    {
        var images = _faker.Generate(count);
        if (propertyId.HasValue)
        {
            foreach (var image in images)
            {
                image.IdProperty = propertyId.Value;
            }
        }
        return images;
    }

    /// <summary>
    /// Creates enabled property images for a property
    /// </summary>
    public static List<PropertyImage> EnabledForProperty(int propertyId, int count = 3)
    {
        var images = _faker.Generate(count);
        foreach (var image in images)
        {
            image.IdProperty = propertyId;
            image.Enabled = true;
        }
        return images;
    }

    /// <summary>
    /// Creates disabled property images for a property
    /// </summary>
    public static List<PropertyImage> DisabledForProperty(int propertyId, int count = 2)
    {
        var images = _faker.Generate(count);
        foreach (var image in images)
        {
            image.IdProperty = propertyId;
            image.Enabled = false;
        }
        return images;
    }

    /// <summary>
    /// Creates mixed enabled/disabled property images
    /// </summary>
    public static List<PropertyImage> MixedEnabledStatus(int propertyId, int enabledCount = 2, int disabledCount = 1)
    {
        var images = new List<PropertyImage>();

        // Add enabled images
        images.AddRange(EnabledForProperty(propertyId, enabledCount));

        // Add disabled images
        images.AddRange(DisabledForProperty(propertyId, disabledCount));

        return images;
    }

    /// <summary>
    /// Creates property images with different file types
    /// </summary>
    public static List<PropertyImage> DifferentFileTypes(int propertyId)
    {
        var jpgImage = JpgImage();
        jpgImage.IdProperty = propertyId;

        var pngImage = PngImage();
        pngImage.IdProperty = propertyId;

        var gifImage = GifImage();
        gifImage.IdProperty = propertyId;

        var webpImage = WebPImage();
        webpImage.IdProperty = propertyId;

        return new List<PropertyImage>
        {
            jpgImage,
            pngImage,
            gifImage,
            webpImage
        };
    }

    /// <summary>
    /// Creates property images representing different rooms
    /// </summary>
    public static List<PropertyImage> DifferentRooms(int propertyId)
    {
        var exteriorImage = ExteriorImage();
        exteriorImage.IdProperty = propertyId;

        var interiorImage = InteriorImage();
        interiorImage.IdProperty = propertyId;

        var kitchenImage = KitchenImage();
        kitchenImage.IdProperty = propertyId;

        var bedroomImage = BedroomImage();
        bedroomImage.IdProperty = propertyId;

        var bathroomImage = BathroomImage();
        bathroomImage.IdProperty = propertyId;

        return new List<PropertyImage>
        {
            exteriorImage,
            interiorImage,
            kitchenImage,
            bedroomImage,
            bathroomImage
        };
    }

    /// <summary>
    /// Creates property images uploaded at different times
    /// </summary>
    public static List<PropertyImage> DifferentUploadTimes(int propertyId, int count = 3)
    {
        var images = new List<PropertyImage>();
        var faker = new Faker();

        for (int i = 0; i < count; i++)
        {
            var image = _faker.Generate();
            image.IdProperty = propertyId;
            image.CreatedAt = DateTime.UtcNow.AddDays(-faker.Random.Int(1, 100));
            images.Add(image);
        }

        return images.OrderBy(img => img.CreatedAt).ToList();
    }

    /// <summary>
    /// Creates property images for multiple properties
    /// </summary>
    public static List<PropertyImage> ForMultipleProperties(List<int> propertyIds, int imagesPerProperty = 2)
    {
        var images = new List<PropertyImage>();

        foreach (var propertyId in propertyIds)
        {
            images.AddRange(Multiple(imagesPerProperty, propertyId));
        }

        return images;
    }

    /// <summary>
    /// Creates property images for testing pagination
    /// </summary>
    public static List<PropertyImage> ForPagination(int propertyId, int totalCount = 20)
    {
        var images = new List<PropertyImage>();
        for (int i = 1; i <= totalCount; i++)
        {
            var image = _faker.Generate();
            image.IdPropertyImage = i;
            image.IdProperty = propertyId;
            image.File = $"{Guid.NewGuid()}_image_{i:D2}.jpg";
            images.Add(image);
        }
        return images;
    }

    #endregion
}