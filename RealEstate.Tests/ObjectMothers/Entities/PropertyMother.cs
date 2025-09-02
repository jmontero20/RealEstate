using Bogus;
using RealEstate.Domain.Entities;

namespace RealEstate.Tests.ObjectMothers.Entities;

public static class PropertyMother
{
    private static readonly Faker<Property> _faker = new Faker<Property>()
        .RuleFor(p => p.IdProperty, f => f.Random.Int(1, 1000))
        .RuleFor(p => p.Name, f => f.Commerce.Product() + " " + f.Address.BuildingNumber())
        .RuleFor(p => p.Address, f => f.Address.FullAddress())
        .RuleFor(p => p.Price, f => f.Random.Decimal(50000m, 2000000m))
        .RuleFor(p => p.CodeInternal, f => f.Random.AlphaNumeric(10).ToUpper())
        .RuleFor(p => p.Year, f => f.Random.Int(1900, DateTime.Now.Year))
        .RuleFor(p => p.IdOwner, f => f.Random.Int(1, 100))
        .RuleFor(p => p.CreatedAt, f => f.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now))
        .RuleFor(p => p.UpdatedAt, f => null)
        .RuleFor(p => p.IsDeleted, f => false);

    #region Basic Scenarios

    /// <summary>
    /// Creates a simple valid property without relationships
    /// </summary>
    public static Property Simple()
    {
        return _faker.Generate();
    }

    /// <summary>
    /// Creates a property with owner relationship
    /// </summary>
    public static Property WithOwner(int? ownerId = null)
    {
        var property = _faker.Generate();
        if (ownerId.HasValue)
            property.IdOwner = ownerId.Value;

        property.Owner = OwnerMother.Simple();
        property.Owner.IdOwner = property.IdOwner;
        return property;
    }

    /// <summary>
    /// Creates a property with property images
    /// </summary>
    public static Property WithImages(int imageCount = 2)
    {
        var property = _faker.Generate();
        property.PropertyImages = PropertyImageMother.Multiple(imageCount, property.IdProperty);
        return property;
    }

    /// <summary>
    /// Creates a property with property traces
    /// </summary>
    public static Property WithTraces(int traceCount = 2)
    {
        var property = _faker.Generate();
        property.PropertyTraces = PropertyTraceMother.Multiple(traceCount, property.IdProperty);
        return property;
    }

    /// <summary>
    /// Creates a complete property with all relationships
    /// </summary>
    public static Property Complete()
    {
        var property = _faker.Generate();
        property.Owner = OwnerMother.Simple();
        property.Owner.IdOwner = property.IdOwner;
        property.PropertyImages = PropertyImageMother.Multiple(3, property.IdProperty);
        property.PropertyTraces = PropertyTraceMother.Multiple(2, property.IdProperty);
        return property;
    }

    #endregion

    #region Specific Scenarios

    /// <summary>
    /// Creates an expensive luxury property
    /// </summary>
    public static Property Expensive()
    {
        var property = _faker.Generate();
        property.Name = "Luxury Penthouse";
        property.Price = 5000000m;
        property.Year = 2022;
        property.CodeInternal = "LUX001";
        return property;
    }

    /// <summary>
    /// Creates a budget-friendly property
    /// </summary>
    public static Property Cheap()
    {
        var property = _faker.Generate();
        property.Name = "Cozy Studio Apartment";
        property.Price = 75000m;
        property.Year = 1985;
        property.CodeInternal = "BUD001";
        return property;
    }

    /// <summary>
    /// Creates an old property
    /// </summary>
    public static Property Old()
    {
        var property = _faker.Generate();
        property.Name = "Historic Victorian House";
        property.Year = 1890;
        property.Price = 450000m;
        property.CodeInternal = "OLD001";
        return property;
    }

    /// <summary>
    /// Creates a newly built property
    /// </summary>
    public static Property New()
    {
        var property = _faker.Generate();
        property.Name = "Modern Smart Home";
        property.Year = DateTime.Now.Year;
        property.Price = 850000m;
        property.CodeInternal = "NEW001";
        return property;
    }

    /// <summary>
    /// Creates a property in Miami
    /// </summary>
    public static Property InMiami()
    {
        var property = _faker.Generate();
        property.Name = "Miami Beach Condo";
        property.Address = "100 Biscayne Blvd, Miami, FL 33132";
        property.Price = 750000m;
        property.CodeInternal = "MIA001";
        return property;
    }

    /// <summary>
    /// Creates a property in New York
    /// </summary>
    public static Property InNewYork()
    {
        var property = _faker.Generate();
        property.Name = "Manhattan Loft";
        property.Address = "200 Central Park West, New York, NY 10024";
        property.Price = 1250000m;
        property.CodeInternal = "NYC001";
        return property;
    }

    #endregion

    #region Validation Scenarios

    /// <summary>
    /// Creates a property with invalid empty name
    /// </summary>
    public static Property WithInvalidName()
    {
        var property = _faker.Generate();
        property.Name = string.Empty;
        return property;
    }

    /// <summary>
    /// Creates a property with name too long
    /// </summary>
    public static Property WithLongName()
    {
        var property = _faker.Generate();
        property.Name = new string('a', 201); // Exceeds 200 character limit
        return property;
    }

    /// <summary>
    /// Creates a property with invalid zero price
    /// </summary>
    public static Property WithInvalidPrice()
    {
        var property = _faker.Generate();
        property.Price = 0m;
        return property;
    }

    /// <summary>
    /// Creates a property with negative price
    /// </summary>
    public static Property WithNegativePrice()
    {
        var property = _faker.Generate();
        property.Price = -100000m;
        return property;
    }

    /// <summary>
    /// Creates a property with duplicate code
    /// </summary>
    public static Property WithDuplicateCode()
    {
        var property = _faker.Generate();
        property.CodeInternal = "DUPLICATE001";
        return property;
    }

    /// <summary>
    /// Creates a property with future year
    /// </summary>
    public static Property WithFutureYear()
    {
        var property = _faker.Generate();
        property.Year = DateTime.Now.Year + 5;
        return property;
    }

    /// <summary>
    /// Creates a property with year too old
    /// </summary>
    public static Property WithOldYear()
    {
        var property = _faker.Generate();
        property.Year = 1800; // Before minimum allowed
        return property;
    }

    #endregion

    #region State Scenarios

    /// <summary>
    /// Creates a soft-deleted property
    /// </summary>
    public static Property Deleted()
    {
        var property = _faker.Generate();
        property.IsDeleted = true;
        return property;
    }

    /// <summary>
    /// Creates a recently updated property
    /// </summary>
    public static Property Updated()
    {
        var property = _faker.Generate();
        property.UpdatedAt = DateTime.UtcNow;
        return property;
    }

    /// <summary>
    /// Creates a property that was created today
    /// </summary>
    public static Property CreatedToday()
    {
        var property = _faker.Generate();
        property.CreatedAt = DateTime.UtcNow.Date;
        return property;
    }

    #endregion

    #region Custom Scenarios

    /// <summary>
    /// Creates a property with specific ID
    /// </summary>
    public static Property WithId(int id)
    {
        var property = _faker.Generate();
        property.IdProperty = id;
        return property;
    }

    /// <summary>
    /// Creates a property with specific owner ID
    /// </summary>
    public static Property WithOwnerId(int ownerId)
    {
        var property = _faker.Generate();
        property.IdOwner = ownerId;
        return property;
    }

    /// <summary>
    /// Creates a property with specific code internal
    /// </summary>
    public static Property WithCodeInternal(string codeInternal)
    {
        var property = _faker.Generate();
        property.CodeInternal = codeInternal;
        return property;
    }

    /// <summary>
    /// Creates a property within price range
    /// </summary>
    public static Property WithPriceRange(decimal minPrice, decimal maxPrice)
    {
        var property = _faker.Generate();
        property.Price = new Faker().Random.Decimal(minPrice, maxPrice);
        return property;
    }

    /// <summary>
    /// Creates a property within year range
    /// </summary>
    public static Property WithYearRange(int minYear, int maxYear)
    {
        var property = _faker.Generate();
        property.Year = new Faker().Random.Int(minYear, maxYear);
        return property;
    }

    #endregion

    #region Lists

    /// <summary>
    /// Creates multiple properties
    /// </summary>
    public static List<Property> Multiple(int count = 3)
    {
        return _faker.Generate(count);
    }

    /// <summary>
    /// Creates properties suitable for filtering tests
    /// </summary>
    public static List<Property> ForFiltering()
    {
        var properties = new List<Property>
        {
            // Property 1: Expensive in Miami, new
            new Property
            {
                IdProperty = 1,
                Name = "Luxury Miami Penthouse",
                Address = "Miami Beach, FL",
                Price = 2500000m,
                CodeInternal = "MIA001",
                Year = 2022,
                IdOwner = 1,
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                IsDeleted = false
            },
            // Property 2: Medium price in NYC, medium age
            new Property
            {
                IdProperty = 2,
                Name = "Manhattan Apartment",
                Address = "New York, NY",
                Price = 850000m,
                CodeInternal = "NYC001",
                Year = 2010,
                IdOwner = 2,
                CreatedAt = DateTime.UtcNow.AddDays(-60),
                IsDeleted = false
            },
            // Property 3: Budget in LA, old
            new Property
            {
                IdProperty = 3,
                Name = "LA Studio",
                Address = "Los Angeles, CA",
                Price = 200000m,
                CodeInternal = "LA001",
                Year = 1990,
                IdOwner = 3,
                CreatedAt = DateTime.UtcNow.AddDays(-90),
                IsDeleted = false
            }
        };

        return properties;
    }

    /// <summary>
    /// Creates properties for pagination tests
    /// </summary>
    public static List<Property> ForPagination(int totalCount = 20)
    {
        var properties = new List<Property>();
        for (int i = 1; i <= totalCount; i++)
        {
            var property = _faker.Generate();
            property.IdProperty = i;
            property.Name = $"Property {i:D2}";
            property.CodeInternal = $"PROP{i:D3}";
            properties.Add(property);
        }
        return properties;
    }

    /// <summary>
    /// Creates properties with same owner
    /// </summary>
    public static List<Property> WithSameOwner(int ownerId, int count = 3)
    {
        var properties = _faker.Generate(count);
        foreach (var property in properties)
        {
            property.IdOwner = ownerId;
        }
        return properties;
    }

    /// <summary>
    /// Creates properties with images and traces
    /// </summary>
    public static List<Property> CompleteList(int count = 3)
    {
        var properties = new List<Property>();
        for (int i = 1; i <= count; i++)
        {
            var property = Complete();
            property.IdProperty = i;
            property.IdOwner = i;
            properties.Add(property);
        }
        return properties;
    }

    #endregion
}