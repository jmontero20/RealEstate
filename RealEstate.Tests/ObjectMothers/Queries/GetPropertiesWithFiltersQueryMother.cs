using Bogus;
using RealEstate.Application.UsecCases.Property.Queries.GetPropertiesWithFilters;

namespace RealEstate.Tests.ObjectMothers.Queries;

public static class GetPropertiesWithFiltersQueryMother
{
    private static readonly Faker _faker = new Faker();

    #region Basic Scenarios

    /// <summary>
    /// Creates a query without any filters (returns all properties)
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithoutFilters()
    {
        return new GetPropertiesWithFiltersQuery(
            Name: null,
            Address: null,
            MinPrice: null,
            MaxPrice: null,
            MinYear: null,
            MaxYear: null,
            OwnerId: null,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query with all possible filters
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithAllFilters()
    {
        return new GetPropertiesWithFiltersQuery(
            Name: "Luxury",
            Address: "Miami",
            MinPrice: 100000m,
            MaxPrice: 2000000m,
            MinYear: 2000,
            MaxYear: 2023,
            OwnerId: 1,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a default query with standard pagination
    /// </summary>
    public static GetPropertiesWithFiltersQuery Default()
    {
        return new GetPropertiesWithFiltersQuery();
    }

    #endregion

    #region Name Filter Scenarios

    /// <summary>
    /// Creates a query filtering by specific name
    /// </summary>
    public static GetPropertiesWithFiltersQuery ByName(string name)
    {
        return new GetPropertiesWithFiltersQuery(
            Name: name,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query filtering by luxury properties
    /// </summary>
    public static GetPropertiesWithFiltersQuery ByLuxuryName()
    {
        return ByName("Luxury");
    }

    /// <summary>
    /// Creates a query filtering by apartment properties
    /// </summary>
    public static GetPropertiesWithFiltersQuery ByApartmentName()
    {
        return ByName("Apartment");
    }

    /// <summary>
    /// Creates a query filtering by condo properties
    /// </summary>
    public static GetPropertiesWithFiltersQuery ByCondoName()
    {
        return ByName("Condo");
    }

    /// <summary>
    /// Creates a query with partial name search
    /// </summary>
    public static GetPropertiesWithFiltersQuery ByPartialName(string partialName = "Modern")
    {
        return ByName(partialName);
    }

    #endregion

    #region Address Filter Scenarios

    /// <summary>
    /// Creates a query filtering by specific address
    /// </summary>
    public static GetPropertiesWithFiltersQuery ByAddress(string address)
    {
        return new GetPropertiesWithFiltersQuery(
            Address: address,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query filtering by Miami properties
    /// </summary>
    public static GetPropertiesWithFiltersQuery InMiami()
    {
        return ByAddress("Miami");
    }

    /// <summary>
    /// Creates a query filtering by New York properties
    /// </summary>
    public static GetPropertiesWithFiltersQuery InNewYork()
    {
        return ByAddress("New York");
    }

    /// <summary>
    /// Creates a query filtering by Los Angeles properties
    /// </summary>
    public static GetPropertiesWithFiltersQuery InLosAngeles()
    {
        return ByAddress("Los Angeles");
    }

    /// <summary>
    /// Creates a query filtering by Florida properties
    /// </summary>
    public static GetPropertiesWithFiltersQuery InFlorida()
    {
        return ByAddress("FL");
    }

    /// <summary>
    /// Creates a query filtering by California properties
    /// </summary>
    public static GetPropertiesWithFiltersQuery InCalifornia()
    {
        return ByAddress("CA");
    }

    #endregion

    #region Price Filter Scenarios

    /// <summary>
    /// Creates a query filtering by price range
    /// </summary>
    public static GetPropertiesWithFiltersQuery ByPriceRange(decimal minPrice, decimal maxPrice)
    {
        return new GetPropertiesWithFiltersQuery(
            MinPrice: minPrice,
            MaxPrice: maxPrice,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query filtering by budget properties (under $200K)
    /// </summary>
    public static GetPropertiesWithFiltersQuery BudgetProperties()
    {
        return ByPriceRange(1m, 200000m);
    }

    /// <summary>
    /// Creates a query filtering by mid-range properties ($200K-$800K)
    /// </summary>
    public static GetPropertiesWithFiltersQuery MidRangeProperties()
    {
        return ByPriceRange(200000m, 800000m);
    }

    /// <summary>
    /// Creates a query filtering by luxury properties ($800K+)
    /// </summary>
    public static GetPropertiesWithFiltersQuery LuxuryProperties()
    {
        return new GetPropertiesWithFiltersQuery(
            MinPrice: 800000m,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query filtering by ultra-luxury properties ($2M+)
    /// </summary>
    public static GetPropertiesWithFiltersQuery UltraLuxuryProperties()
    {
        return new GetPropertiesWithFiltersQuery(
            MinPrice: 2000000m,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query with only minimum price
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithMinPrice(decimal minPrice)
    {
        return new GetPropertiesWithFiltersQuery(
            MinPrice: minPrice,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query with only maximum price
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithMaxPrice(decimal maxPrice)
    {
        return new GetPropertiesWithFiltersQuery(
            MaxPrice: maxPrice,
            PageNumber: 1,
            PageSize: 10
        );
    }

    #endregion

    #region Year Filter Scenarios

    /// <summary>
    /// Creates a query filtering by year range
    /// </summary>
    public static GetPropertiesWithFiltersQuery ByYearRange(int minYear, int maxYear)
    {
        return new GetPropertiesWithFiltersQuery(
            MinYear: minYear,
            MaxYear: maxYear,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query filtering by specific year
    /// </summary>
    public static GetPropertiesWithFiltersQuery ByYear(int year)
    {
        return ByYearRange(year, year);
    }

    /// <summary>
    /// Creates a query filtering by new properties (built in last 5 years)
    /// </summary>
    public static GetPropertiesWithFiltersQuery NewProperties()
    {
        var currentYear = DateTime.Now.Year;
        return ByYearRange(currentYear - 5, currentYear);
    }

    /// <summary>
    /// Creates a query filtering by modern properties (built after 2000)
    /// </summary>
    public static GetPropertiesWithFiltersQuery ModernProperties()
    {
        return new GetPropertiesWithFiltersQuery(
            MinYear: 2000,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query filtering by vintage properties (built before 1980)
    /// </summary>
    public static GetPropertiesWithFiltersQuery VintageProperties()
    {
        return new GetPropertiesWithFiltersQuery(
            MaxYear: 1980,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query filtering by properties built this year
    /// </summary>
    public static GetPropertiesWithFiltersQuery CurrentYear()
    {
        return ByYear(DateTime.Now.Year);
    }

    /// <summary>
    /// Creates a query filtering by properties built last year
    /// </summary>
    public static GetPropertiesWithFiltersQuery LastYear()
    {
        return ByYear(DateTime.Now.Year - 1);
    }

    #endregion

    #region Owner Filter Scenarios

    /// <summary>
    /// Creates a query filtering by specific owner
    /// </summary>
    public static GetPropertiesWithFiltersQuery ByOwner(int ownerId)
    {
        return new GetPropertiesWithFiltersQuery(
            OwnerId: ownerId,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query filtering by John Smith's properties (Owner ID 1)
    /// </summary>
    public static GetPropertiesWithFiltersQuery ByJohnSmith()
    {
        return ByOwner(1);
    }

    /// <summary>
    /// Creates a query filtering by Sarah Johnson's properties (Owner ID 2)
    /// </summary>
    public static GetPropertiesWithFiltersQuery BySarahJohnson()
    {
        return ByOwner(2);
    }

    /// <summary>
    /// Creates a query filtering by Michael Brown's properties (Owner ID 3)
    /// </summary>
    public static GetPropertiesWithFiltersQuery ByMichaelBrown()
    {
        return ByOwner(3);
    }

    #endregion

    #region Pagination Scenarios

    /// <summary>
    /// Creates a query for the first page
    /// </summary>
    public static GetPropertiesWithFiltersQuery FirstPage(int pageSize = 10)
    {
        return new GetPropertiesWithFiltersQuery(
            PageNumber: 1,
            PageSize: pageSize
        );
    }

    /// <summary>
    /// Creates a query for the second page
    /// </summary>
    public static GetPropertiesWithFiltersQuery SecondPage(int pageSize = 10)
    {
        return new GetPropertiesWithFiltersQuery(
            PageNumber: 2,
            PageSize: pageSize
        );
    }

    /// <summary>
    /// Creates a query for a specific page
    /// </summary>
    public static GetPropertiesWithFiltersQuery Page(int pageNumber, int pageSize = 10)
    {
        return new GetPropertiesWithFiltersQuery(
            PageNumber: pageNumber,
            PageSize: pageSize
        );
    }

    /// <summary>
    /// Creates a query with large page size
    /// </summary>
    public static GetPropertiesWithFiltersQuery LargePage()
    {
        return new GetPropertiesWithFiltersQuery(
            PageNumber: 1,
            PageSize: 100
        );
    }

    /// <summary>
    /// Creates a query with small page size
    /// </summary>
    public static GetPropertiesWithFiltersQuery SmallPage()
    {
        return new GetPropertiesWithFiltersQuery(
            PageNumber: 1,
            PageSize: 5
        );
    }

    /// <summary>
    /// Creates a query with single item page
    /// </summary>
    public static GetPropertiesWithFiltersQuery SingleItemPage()
    {
        return new GetPropertiesWithFiltersQuery(
            PageNumber: 1,
            PageSize: 1
        );
    }

    #endregion

    #region Combined Filter Scenarios

    /// <summary>
    /// Creates a query for luxury properties in Miami
    /// </summary>
    public static GetPropertiesWithFiltersQuery LuxuryInMiami()
    {
        return new GetPropertiesWithFiltersQuery(
            Name: "Luxury",
            Address: "Miami",
            MinPrice: 500000m,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query for modern apartments under $1M
    /// </summary>
    public static GetPropertiesWithFiltersQuery ModernApartmentsUnder1M()
    {
        return new GetPropertiesWithFiltersQuery(
            Name: "Apartment",
            MaxPrice: 1000000m,
            MinYear: 2000,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query for John Smith's luxury properties
    /// </summary>
    public static GetPropertiesWithFiltersQuery JohnSmithLuxury()
    {
        return new GetPropertiesWithFiltersQuery(
            Name: "Luxury",
            MinPrice: 750000m,
            OwnerId: 1,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query for new properties in expensive areas
    /// </summary>
    public static GetPropertiesWithFiltersQuery NewExpensiveProperties()
    {
        var currentYear = DateTime.Now.Year;
        return new GetPropertiesWithFiltersQuery(
            MinPrice: 800000m,
            MinYear: currentYear - 3,
            MaxYear: currentYear,
            PageNumber: 1,
            PageSize: 10
        );
    }

    #endregion

    #region Invalid Scenarios

    /// <summary>
    /// Creates a query with invalid pagination (page number zero)
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithInvalidPageNumber()
    {
        return new GetPropertiesWithFiltersQuery(
            PageNumber: 0,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query with invalid pagination (negative page number)
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithNegativePageNumber()
    {
        return new GetPropertiesWithFiltersQuery(
            PageNumber: -1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query with invalid page size (zero)
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithInvalidPageSize()
    {
        return new GetPropertiesWithFiltersQuery(
            PageNumber: 1,
            PageSize: 0
        );
    }

    /// <summary>
    /// Creates a query with invalid page size (negative)
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithNegativePageSize()
    {
        return new GetPropertiesWithFiltersQuery(
            PageNumber: 1,
            PageSize: -5
        );
    }

    /// <summary>
    /// Creates a query with page size exceeding maximum (over 100)
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithExcessivePageSize()
    {
        return new GetPropertiesWithFiltersQuery(
            PageNumber: 1,
            PageSize: 150
        );
    }

    /// <summary>
    /// Creates a query with invalid price range (min > max)
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithInvalidPriceRange()
    {
        return new GetPropertiesWithFiltersQuery(
            MinPrice: 800000m,
            MaxPrice: 500000m, // Max < Min
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query with invalid year range (min > max)
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithInvalidYearRange()
    {
        return new GetPropertiesWithFiltersQuery(
            MinYear: 2020,
            MaxYear: 2010, // Max < Min
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query with negative price values
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithNegativePrices()
    {
        return new GetPropertiesWithFiltersQuery(
            MinPrice: -100000m,
            MaxPrice: -50000m,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query with invalid owner ID (negative)
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithInvalidOwnerId()
    {
        return new GetPropertiesWithFiltersQuery(
            OwnerId: -1,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query with zero owner ID
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithZeroOwnerId()
    {
        return new GetPropertiesWithFiltersQuery(
            OwnerId: 0,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query with non-existent owner ID
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithNonExistentOwnerId()
    {
        return new GetPropertiesWithFiltersQuery(
            OwnerId: 999999,
            PageNumber: 1,
            PageSize: 10
        );
    }

    /// <summary>
    /// Creates a query with names/addresses that are too long
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithLongStrings()
    {
        return new GetPropertiesWithFiltersQuery(
            Name: new string('a', 300), // Exceeds typical limits
            Address: new string('b', 600), // Exceeds typical limits
            PageNumber: 1,
            PageSize: 10
        );
    }

    #endregion

    #region Edge Cases

    /// <summary>
    /// Creates a query with boundary values (minimum valid)
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithMinimumValues()
    {
        return new GetPropertiesWithFiltersQuery(
            MinPrice: 0m,
            MaxPrice: 0.01m,
            MinYear: 1801,
            MaxYear: 1801,
            OwnerId: 1,
            PageNumber: 1,
            PageSize: 1
        );
    }

    /// <summary>
    /// Creates a query with boundary values (maximum valid)
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithMaximumValues()
    {
        return new GetPropertiesWithFiltersQuery(
            MinPrice: 999999999.98m,
            MaxPrice: 999999999.99m,
            MinYear: DateTime.Now.Year,
            MaxYear: DateTime.Now.Year,
            OwnerId: int.MaxValue,
            PageNumber: 1,
            PageSize: 100
        );
    }

    /// <summary>
    /// Creates a query that should return no results
    /// </summary>
    public static GetPropertiesWithFiltersQuery ThatReturnsNoResults()
    {
        return new GetPropertiesWithFiltersQuery(
            Name: "NonExistentPropertyNameThatWillNeverMatch123456789",
            MinPrice: 50000000m, // Extremely high price
            MaxPrice: 100000000m,
            PageNumber: 1,
            PageSize: 10
        );
    }

    #endregion

    #region Random Scenarios

    /// <summary>
    /// Creates a query with random valid filters
    /// </summary>
    public static GetPropertiesWithFiltersQuery WithRandomFilters()
    {
        var minPrice = _faker.Random.Decimal(50000m, 500000m);
        var maxPrice = minPrice + _faker.Random.Decimal(100000m, 1000000m);
        var minYear = _faker.Random.Int(1950, 2010);
        var maxYear = minYear + _faker.Random.Int(0, 20);

        return new GetPropertiesWithFiltersQuery(
            Name: _faker.Random.Bool() ? _faker.PickRandom("Luxury", "Modern", "Apartment", "Condo") : null,
            Address: _faker.Random.Bool() ? _faker.PickRandom("Miami", "New York", "Los Angeles", "FL", "CA") : null,
            MinPrice: _faker.Random.Bool() ? minPrice : null,
            MaxPrice: _faker.Random.Bool() ? maxPrice : null,
            MinYear: _faker.Random.Bool() ? minYear : null,
            MaxYear: _faker.Random.Bool() ? maxYear : null,
            OwnerId: _faker.Random.Bool() ? _faker.Random.Int(1, 10) : null,
            PageNumber: _faker.Random.Int(1, 5),
            PageSize: _faker.Random.Int(5, 50)
        );
    }

    /// <summary>
    /// Creates multiple random queries for testing
    /// </summary>
    public static List<GetPropertiesWithFiltersQuery> MultipleRandom(int count = 5)
    {
        var queries = new List<GetPropertiesWithFiltersQuery>();
        for (int i = 0; i < count; i++)
        {
            queries.Add(WithRandomFilters());
        }
        return queries;
    }

    #endregion
}