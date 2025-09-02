using Bogus;
using RealEstate.Application.UsecCases.Property.Commands.CreateProperty;

namespace RealEstate.Tests.ObjectMothers.Responses;

public static class CreatePropertyResponseMother
{
    private static readonly Faker _faker = new Faker();

    #region Basic Scenarios

    /// <summary>
    /// Creates a simple valid CreatePropertyResponse
    /// </summary>
    public static CreatePropertyResponse Valid()
    {
        return new CreatePropertyResponse
        {
            PropertyId = _faker.Random.Int(1, 1000),
            Name = _faker.Commerce.Product() + " " + _faker.Address.BuildingNumber(),
            CodeInternal = _faker.Random.AlphaNumeric(10).ToUpper(),
            Price = _faker.Random.Decimal(50000m, 2000000m),
            OwnerName = _faker.Name.FullName(),
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response for specific property ID
    /// </summary>
    public static CreatePropertyResponse ForProperty(int propertyId)
    {
        var response = Valid();
        response.PropertyId = propertyId;
        return response;
    }

    /// <summary>
    /// Creates a response with specific owner name
    /// </summary>
    public static CreatePropertyResponse WithOwner(string ownerName)
    {
        var response = Valid();
        response.OwnerName = ownerName;
        return response;
    }

    /// <summary>
    /// Creates a response with specific creation date
    /// </summary>
    public static CreatePropertyResponse WithCreationDate(DateTime createdAt)
    {
        var response = Valid();
        response.CreatedAt = createdAt;
        return response;
    }

    /// <summary>
    /// Creates a response created today
    /// </summary>
    public static CreatePropertyResponse CreatedToday()
    {
        var response = Valid();
        response.CreatedAt = DateTime.UtcNow.Date;
        return response;
    }

    /// <summary>
    /// Creates a response created now
    /// </summary>
    public static CreatePropertyResponse CreatedNow()
    {
        var response = Valid();
        response.CreatedAt = DateTime.UtcNow;
        return response;
    }

    #endregion

    #region Named Scenarios (Based on Seed Data)

    /// <summary>
    /// Creates a response for Miami property (like seed data)
    /// </summary>
    public static CreatePropertyResponse MiamiProperty()
    {
        return new CreatePropertyResponse
        {
            PropertyId = 1,
            Name = "Luxury Apartment Downtown",
            CodeInternal = "MIA001",
            Price = 750000.00m,
            OwnerName = "John Smith",
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response for New York property (like seed data)
    /// </summary>
    public static CreatePropertyResponse NewYorkProperty()
    {
        return new CreatePropertyResponse
        {
            PropertyId = 2,
            Name = "Modern Condo Manhattan",
            CodeInternal = "NYC001",
            Price = 1250000.00m,
            OwnerName = "Sarah Johnson",
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response for Los Angeles property (like seed data)
    /// </summary>
    public static CreatePropertyResponse LosAngelesProperty()
    {
        return new CreatePropertyResponse
        {
            PropertyId = 3,
            Name = "Beverly Hills Villa",
            CodeInternal = "LA001",
            Price = 3500000.00m,
            OwnerName = "Michael Brown",
            CreatedAt = DateTime.UtcNow
        };
    }

    #endregion

    #region Price-based Scenarios

    /// <summary>
    /// Creates a response for luxury property (high price)
    /// </summary>
    public static CreatePropertyResponse LuxuryProperty()
    {
        return new CreatePropertyResponse
        {
            PropertyId = _faker.Random.Int(1, 1000),
            Name = "Luxury Penthouse Suite",
            CodeInternal = "LUX001",
            Price = 2500000m,
            OwnerName = "Elite Owner",
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response for budget property (low price)
    /// </summary>
    public static CreatePropertyResponse BudgetProperty()
    {
        return new CreatePropertyResponse
        {
            PropertyId = _faker.Random.Int(1, 1000),
            Name = "Cozy Studio Apartment",
            CodeInternal = "BUD001",
            Price = 75000m,
            OwnerName = "Budget Owner",
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response for mid-range property
    /// </summary>
    public static CreatePropertyResponse MidRangeProperty()
    {
        return new CreatePropertyResponse
        {
            PropertyId = _faker.Random.Int(1, 1000),
            Name = "Family Home",
            CodeInternal = "MID001",
            Price = 450000m,
            OwnerName = "Family Owner",
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response with specific price
    /// </summary>
    public static CreatePropertyResponse WithPrice(decimal price)
    {
        var response = Valid();
        response.Price = price;
        return response;
    }

    /// <summary>
    /// Creates a response with minimum valid price
    /// </summary>
    public static CreatePropertyResponse WithMinimumPrice()
    {
        var response = Valid();
        response.Price = 0.01m;
        return response;
    }

    /// <summary>
    /// Creates a response with maximum valid price
    /// </summary>
    public static CreatePropertyResponse WithMaximumPrice()
    {
        var response = Valid();
        response.Price = 999999999.99m;
        return response;
    }

    #endregion

    #region Property Type Scenarios

    /// <summary>
    /// Creates a response for apartment property
    /// </summary>
    public static CreatePropertyResponse ApartmentProperty()
    {
        return new CreatePropertyResponse
        {
            PropertyId = _faker.Random.Int(1, 1000),
            Name = "Modern Downtown Apartment",
            CodeInternal = "APT001",
            Price = _faker.Random.Decimal(200000m, 800000m),
            OwnerName = _faker.Name.FullName(),
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response for condo property
    /// </summary>
    public static CreatePropertyResponse CondoProperty()
    {
        return new CreatePropertyResponse
        {
            PropertyId = _faker.Random.Int(1, 1000),
            Name = "Waterfront Condo",
            CodeInternal = "CON001",
            Price = _faker.Random.Decimal(300000m, 1200000m),
            OwnerName = _faker.Name.FullName(),
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response for house property
    /// </summary>
    public static CreatePropertyResponse HouseProperty()
    {
        return new CreatePropertyResponse
        {
            PropertyId = _faker.Random.Int(1, 1000),
            Name = "Single Family House",
            CodeInternal = "HSE001",
            Price = _faker.Random.Decimal(250000m, 1500000m),
            OwnerName = _faker.Name.FullName(),
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response for villa property
    /// </summary>
    public static CreatePropertyResponse VillaProperty()
    {
        return new CreatePropertyResponse
        {
            PropertyId = _faker.Random.Int(1, 1000),
            Name = "Luxury Villa Estate",
            CodeInternal = "VIL001",
            Price = _faker.Random.Decimal(1000000m, 5000000m),
            OwnerName = _faker.Name.FullName(),
            CreatedAt = DateTime.UtcNow
        };
    }

    #endregion

    #region Owner-based Scenarios

    /// <summary>
    /// Creates a response for John Smith's property
    /// </summary>
    public static CreatePropertyResponse JohnSmithProperty()
    {
        var response = Valid();
        response.OwnerName = "John Smith";
        return response;
    }

    /// <summary>
    /// Creates a response for Sarah Johnson's property
    /// </summary>
    public static CreatePropertyResponse SarahJohnsonProperty()
    {
        var response = Valid();
        response.OwnerName = "Sarah Johnson";
        return response;
    }

    /// <summary>
    /// Creates a response for Michael Brown's property
    /// </summary>
    public static CreatePropertyResponse MichaelBrownProperty()
    {
        var response = Valid();
        response.OwnerName = "Michael Brown";
        return response;
    }

    /// <summary>
    /// Creates a response with unknown owner
    /// </summary>
    public static CreatePropertyResponse WithUnknownOwner()
    {
        var response = Valid();
        response.OwnerName = "Unknown";
        return response;
    }

    #endregion

    #region Code Internal Scenarios

    /// <summary>
    /// Creates a response with specific code internal
    /// </summary>
    public static CreatePropertyResponse WithCodeInternal(string codeInternal)
    {
        var response = Valid();
        response.CodeInternal = codeInternal;
        return response;
    }

    /// <summary>
    /// Creates a response with Miami code pattern
    /// </summary>
    public static CreatePropertyResponse WithMiamiCode()
    {
        return WithCodeInternal("MIA001");
    }

    /// <summary>
    /// Creates a response with New York code pattern
    /// </summary>
    public static CreatePropertyResponse WithNewYorkCode()
    {
        return WithCodeInternal("NYC001");
    }

    /// <summary>
    /// Creates a response with Los Angeles code pattern
    /// </summary>
    public static CreatePropertyResponse WithLosAngelesCode()
    {
        return WithCodeInternal("LA001");
    }

    /// <summary>
    /// Creates a response with sequential code
    /// </summary>
    public static CreatePropertyResponse WithSequentialCode(int number)
    {
        return WithCodeInternal($"PROP{number:D3}");
    }

    #endregion

    #region Custom Scenarios

    /// <summary>
    /// Creates a response with all specific values
    /// </summary>
    public static CreatePropertyResponse WithAllValues(
        int propertyId,
        string name,
        string codeInternal,
        decimal price,
        string ownerName,
        DateTime? createdAt = null)
    {
        return new CreatePropertyResponse
        {
            PropertyId = propertyId,
            Name = name,
            CodeInternal = codeInternal,
            Price = price,
            OwnerName = ownerName,
            CreatedAt = createdAt ?? DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response matching a CreatePropertyCommand
    /// </summary>
    public static CreatePropertyResponse FromCommand(CreatePropertyCommand command, int propertyId, string ownerName)
    {
        return new CreatePropertyResponse
        {
            PropertyId = propertyId,
            Name = command.Name,
            CodeInternal = command.CodeInternal,
            Price = command.Price,
            OwnerName = ownerName,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response with specific property ID and name
    /// </summary>
    public static CreatePropertyResponse WithIdAndName(int propertyId, string name)
    {
        var response = Valid();
        response.PropertyId = propertyId;
        response.Name = name;
        return response;
    }

    #endregion

    #region Edge Cases

    /// <summary>
    /// Creates a response with minimum valid values
    /// </summary>
    public static CreatePropertyResponse WithMinimumValues()
    {
        return new CreatePropertyResponse
        {
            PropertyId = 1,
            Name = "A", // Minimum name
            CodeInternal = "1", // Minimum code
            Price = 0.01m, // Minimum price
            OwnerName = "A", // Minimum owner name
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response with boundary values
    /// </summary>
    public static CreatePropertyResponse WithBoundaryValues()
    {
        return new CreatePropertyResponse
        {
            PropertyId = int.MaxValue,
            Name = new string('A', 200), // Maximum name length
            CodeInternal = new string('C', 50), // Maximum code length
            Price = 999999999.99m, // Maximum price
            OwnerName = new string('O', 200), // Maximum owner name length
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a response with very old creation date
    /// </summary>
    public static CreatePropertyResponse CreatedLongAgo()
    {
        var response = Valid();
        response.CreatedAt = DateTime.UtcNow.AddYears(-5);
        return response;
    }

    /// <summary>
    /// Creates a response with precise creation time (with milliseconds)
    /// </summary>
    public static CreatePropertyResponse WithPreciseTime()
    {
        var response = Valid();
        response.CreatedAt = new DateTime(2023, 12, 25, 14, 30, 45, 123, DateTimeKind.Utc);
        return response;
    }

    #endregion

    #region Lists

    /// <summary>
    /// Creates multiple valid responses
    /// </summary>
    public static List<CreatePropertyResponse> Multiple(int count = 3)
    {
        var responses = new List<CreatePropertyResponse>();
        for (int i = 1; i <= count; i++)
        {
            var response = Valid();
            response.PropertyId = i;
            response.CodeInternal = $"PROP{i:D3}";
            responses.Add(response);
        }
        return responses;
    }

    /// <summary>
    /// Creates responses for all seed data properties
    /// </summary>
    public static List<CreatePropertyResponse> SeedDataResponses()
    {
        return new List<CreatePropertyResponse>
        {
            MiamiProperty(),
            NewYorkProperty(),
            LosAngelesProperty()
        };
    }

    /// <summary>
    /// Creates responses with different price ranges
    /// </summary>
    public static List<CreatePropertyResponse> DifferentPriceRanges()
    {
        return new List<CreatePropertyResponse>
        {
            BudgetProperty(),
            MidRangeProperty(),
            LuxuryProperty()
        };
    }

    /// <summary>
    /// Creates responses with different property types
    /// </summary>
    public static List<CreatePropertyResponse> DifferentPropertyTypes()
    {
        return new List<CreatePropertyResponse>
        {
            ApartmentProperty(),
            CondoProperty(),
            HouseProperty(),
            VillaProperty()
        };
    }

    /// <summary>
    /// Creates responses for different owners
    /// </summary>
    public static List<CreatePropertyResponse> DifferentOwners()
    {
        return new List<CreatePropertyResponse>
        {
            JohnSmithProperty(),
            SarahJohnsonProperty(),
            MichaelBrownProperty()
        };
    }

    /// <summary>
    /// Creates responses created at different times
    /// </summary>
    public static List<CreatePropertyResponse> CreatedAtDifferentTimes()
    {
        return new List<CreatePropertyResponse>
        {
            WithCreationDate(DateTime.UtcNow.AddDays(-30)),
            WithCreationDate(DateTime.UtcNow.AddDays(-15)),
            WithCreationDate(DateTime.UtcNow.AddDays(-1)),
            CreatedToday(),
            CreatedNow()
        };
    }

    /// <summary>
    /// Creates sequential responses for bulk testing
    /// </summary>
    public static List<CreatePropertyResponse> Sequential(int startId = 1, int count = 10)
    {
        var responses = new List<CreatePropertyResponse>();
        for (int i = 0; i < count; i++)
        {
            var response = Valid();
            response.PropertyId = startId + i;
            response.Name = $"Property {startId + i:D2}";
            response.CodeInternal = $"PROP{startId + i:D3}";
            responses.Add(response);
        }
        return responses;
    }

    #endregion
}