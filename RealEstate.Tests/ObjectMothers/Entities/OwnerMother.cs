using Bogus;
using RealEstate.Domain.Entities;

namespace RealEstate.Tests.ObjectMothers.Entities;

public static class OwnerMother
{
    private static readonly Faker<Owner> _faker = new Faker<Owner>()
        .RuleFor(o => o.IdOwner, f => f.Random.Int(1, 1000))
        .RuleFor(o => o.Name, f => f.Name.FullName())
        .RuleFor(o => o.Address, f => f.Address.FullAddress())
        .RuleFor(o => o.Birthday, f => f.Date.Between(DateTime.Now.AddYears(-80), DateTime.Now.AddYears(-18)))
        .RuleFor(o => o.Photo, f => null)
        .RuleFor(o => o.CreatedAt, f => f.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now))
        .RuleFor(o => o.UpdatedAt, f => null)
        .RuleFor(o => o.IsDeleted, f => false);

    #region Basic Scenarios

    /// <summary>
    /// Creates a simple valid owner without relationships
    /// </summary>
    public static Owner Simple()
    {
        return _faker.Generate();
    }

    /// <summary>
    /// Creates an owner with properties
    /// </summary>
    public static Owner WithProperties(int propertyCount = 2)
    {
        var owner = _faker.Generate();
        owner.Properties = PropertyMother.WithSameOwner(owner.IdOwner, propertyCount);
        return owner;
    }

    /// <summary>
    /// Creates an owner with a photo
    /// </summary>
    public static Owner WithPhoto()
    {
        var owner = _faker.Generate();
        owner.Photo = $"owner_{owner.IdOwner}_photo.jpg";
        return owner;
    }

    /// <summary>
    /// Creates an owner with all data complete
    /// </summary>
    public static Owner Complete()
    {
        var owner = _faker.Generate();
        owner.Photo = $"owner_{owner.IdOwner}_photo.jpg";
        owner.Properties = PropertyMother.WithSameOwner(owner.IdOwner, 2);
        return owner;
    }

    #endregion

    #region Named Scenarios (Based on Seed Data)

    /// <summary>
    /// Creates John Smith owner (based on seed data)
    /// </summary>
    public static Owner JohnSmith()
    {
        return new Owner
        {
            IdOwner = 1,
            Name = "John Smith",
            Address = "123 Main Street, Miami, FL 33101",
            Birthday = new DateTime(1980, 5, 15),
            Photo = null,
            CreatedAt = DateTime.UtcNow.AddDays(-100),
            IsDeleted = false
        };
    }

    /// <summary>
    /// Creates Sarah Johnson owner (based on seed data)
    /// </summary>
    public static Owner SarahJohnson()
    {
        return new Owner
        {
            IdOwner = 2,
            Name = "Sarah Johnson",
            Address = "456 Oak Avenue, New York, NY 10001",
            Birthday = new DateTime(1975, 11, 22),
            Photo = null,
            CreatedAt = DateTime.UtcNow.AddDays(-90),
            IsDeleted = false
        };
    }

    /// <summary>
    /// Creates Michael Brown owner (based on seed data)
    /// </summary>
    public static Owner MichaelBrown()
    {
        return new Owner
        {
            IdOwner = 3,
            Name = "Michael Brown",
            Address = "789 Pine Road, Los Angeles, CA 90210",
            Birthday = new DateTime(1990, 3, 8),
            Photo = null,
            CreatedAt = DateTime.UtcNow.AddDays(-80),
            IsDeleted = false
        };
    }

    #endregion

    #region Age-based Scenarios

    /// <summary>
    /// Creates a young owner (18-30 years old)
    /// </summary>
    public static Owner Young()
    {
        var owner = _faker.Generate();
        owner.Name = "Alex Young";
        owner.Birthday = DateTime.Now.AddYears(-new Faker().Random.Int(18, 30));
        return owner;
    }

    /// <summary>
    /// Creates a middle-aged owner (30-50 years old)
    /// </summary>
    public static Owner MiddleAged()
    {
        var owner = _faker.Generate();
        owner.Name = "Patricia Middle";
        owner.Birthday = DateTime.Now.AddYears(-new Faker().Random.Int(30, 50));
        return owner;
    }

    /// <summary>
    /// Creates a senior owner (50+ years old)
    /// </summary>
    public static Owner Senior()
    {
        var owner = _faker.Generate();
        owner.Name = "Robert Senior";
        owner.Birthday = DateTime.Now.AddYears(-new Faker().Random.Int(50, 80));
        return owner;
    }

    #endregion

    #region Location-based Scenarios

    /// <summary>
    /// Creates an owner from Miami
    /// </summary>
    public static Owner FromMiami()
    {
        var owner = _faker.Generate();
        owner.Name = "Carlos Miami";
        owner.Address = "Miami Beach, FL 33139";
        return owner;
    }

    /// <summary>
    /// Creates an owner from New York
    /// </summary>
    public static Owner FromNewYork()
    {
        var owner = _faker.Generate();
        owner.Name = "Emily Manhattan";
        owner.Address = "Manhattan, NY 10001";
        return owner;
    }

    /// <summary>
    /// Creates an owner from Los Angeles
    /// </summary>
    public static Owner FromLosAngeles()
    {
        var owner = _faker.Generate();
        owner.Name = "David Hollywood";
        owner.Address = "Beverly Hills, CA 90210";
        return owner;
    }

    #endregion

    #region Validation Scenarios

    /// <summary>
    /// Creates an owner with invalid empty name
    /// </summary>
    public static Owner WithInvalidName()
    {
        var owner = _faker.Generate();
        owner.Name = string.Empty;
        return owner;
    }

    /// <summary>
    /// Creates an owner with name too long
    /// </summary>
    public static Owner WithLongName()
    {
        var owner = _faker.Generate();
        owner.Name = new string('a', 201); // Exceeds 200 character limit
        return owner;
    }

    /// <summary>
    /// Creates an owner with invalid empty address
    /// </summary>
    public static Owner WithInvalidAddress()
    {
        var owner = _faker.Generate();
        owner.Address = string.Empty;
        return owner;
    }

    /// <summary>
    /// Creates an owner with address too long
    /// </summary>
    public static Owner WithLongAddress()
    {
        var owner = _faker.Generate();
        owner.Address = new string('a', 501); // Exceeds 500 character limit
        return owner;
    }

    /// <summary>
    /// Creates an owner with future birthday
    /// </summary>
    public static Owner WithFutureBirthday()
    {
        var owner = _faker.Generate();
        owner.Birthday = DateTime.Now.AddYears(5); // Future date
        return owner;
    }

    /// <summary>
    /// Creates an owner who is underage (under 18)
    /// </summary>
    public static Owner Underage()
    {
        var owner = _faker.Generate();
        owner.Name = "Minor Child";
        owner.Birthday = DateTime.Now.AddYears(-10); // 10 years old
        return owner;
    }

    /// <summary>
    /// Creates an owner who is extremely old
    /// </summary>
    public static Owner VeryOld()
    {
        var owner = _faker.Generate();
        owner.Name = "Ancient Owner";
        owner.Birthday = DateTime.Now.AddYears(-120); // 120 years old
        return owner;
    }

    #endregion

    #region State Scenarios

    /// <summary>
    /// Creates a soft-deleted owner
    /// </summary>
    public static Owner Deleted()
    {
        var owner = _faker.Generate();
        owner.IsDeleted = true;
        return owner;
    }

    /// <summary>
    /// Creates a recently updated owner
    /// </summary>
    public static Owner Updated()
    {
        var owner = _faker.Generate();
        owner.UpdatedAt = DateTime.UtcNow;
        return owner;
    }

    /// <summary>
    /// Creates an owner that was created today
    /// </summary>
    public static Owner CreatedToday()
    {
        var owner = _faker.Generate();
        owner.CreatedAt = DateTime.UtcNow.Date;
        return owner;
    }

    #endregion

    #region Custom Scenarios

    /// <summary>
    /// Creates an owner with specific ID
    /// </summary>
    public static Owner WithId(int id)
    {
        var owner = _faker.Generate();
        owner.IdOwner = id;
        return owner;
    }

    /// <summary>
    /// Creates an owner with specific name
    /// </summary>
    public static Owner WithName(string name)
    {
        var owner = _faker.Generate();
        owner.Name = name;
        return owner;
    }

    /// <summary>
    /// Creates an owner with specific birthday
    /// </summary>
    public static Owner WithBirthday(DateTime birthday)
    {
        var owner = _faker.Generate();
        owner.Birthday = birthday;
        return owner;
    }

    /// <summary>
    /// Creates an owner with specific address
    /// </summary>
    public static Owner WithAddress(string address)
    {
        var owner = _faker.Generate();
        owner.Address = address;
        return owner;
    }

    /// <summary>
    /// Creates an owner born in specific year
    /// </summary>
    public static Owner BornInYear(int year)
    {
        var owner = _faker.Generate();
        owner.Birthday = new DateTime(year, new Faker().Random.Int(1, 12), new Faker().Random.Int(1, 28));
        return owner;
    }

    #endregion

    #region Lists

    /// <summary>
    /// Creates multiple owners
    /// </summary>
    public static List<Owner> Multiple(int count = 3)
    {
        return _faker.Generate(count);
    }

    /// <summary>
    /// Creates the seed data owners
    /// </summary>
    public static List<Owner> SeedData()
    {
        return new List<Owner>
        {
            JohnSmith(),
            SarahJohnson(),
            MichaelBrown()
        };
    }

    /// <summary>
    /// Creates owners with properties for testing relationships
    /// </summary>
    public static List<Owner> WithPropertiesList(int ownerCount = 3, int propertiesPerOwner = 2)
    {
        var owners = new List<Owner>();
        for (int i = 1; i <= ownerCount; i++)
        {
            var owner = _faker.Generate();
            owner.IdOwner = i;
            owner.Properties = PropertyMother.WithSameOwner(i, propertiesPerOwner);
            owners.Add(owner);
        }
        return owners;
    }

    /// <summary>
    /// Creates owners from different age groups
    /// </summary>
    public static List<Owner> DifferentAgeGroups()
    {
        return new List<Owner>
        {
            Young(),
            MiddleAged(),
            Senior()
        };
    }

    /// <summary>
    /// Creates owners from different locations
    /// </summary>
    public static List<Owner> DifferentLocations()
    {
        return new List<Owner>
        {
            FromMiami(),
            FromNewYork(),
            FromLosAngeles()
        };
    }

    /// <summary>
    /// Creates owners for pagination tests
    /// </summary>
    public static List<Owner> ForPagination(int totalCount = 20)
    {
        var owners = new List<Owner>();
        for (int i = 1; i <= totalCount; i++)
        {
            var owner = _faker.Generate();
            owner.IdOwner = i;
            owner.Name = $"Owner {i:D2}";
            owners.Add(owner);
        }
        return owners;
    }

    #endregion
}