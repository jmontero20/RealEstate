using Bogus;
using RealEstate.Domain.Entities;

namespace RealEstate.Tests.ObjectMothers.Entities;

public static class PropertyTraceMother
{
    private static readonly Faker<PropertyTrace> _faker = new Faker<PropertyTrace>()
        .RuleFor(pt => pt.IdPropertyTrace, f => f.Random.Int(1, 10000))
        .RuleFor(pt => pt.IdProperty, f => f.Random.Int(1, 1000))
        .RuleFor(pt => pt.DateSale, f => f.Date.Between(DateTime.Now.AddYears(-5), DateTime.Now))
        .RuleFor(pt => pt.Name, f => f.PickRandom("Property Sale", "Price Change", "Market Update", "Owner Transfer", "Valuation Update"))
        .RuleFor(pt => pt.Value, f => f.Random.Decimal(50000m, 5000000m))
        .RuleFor(pt => pt.Tax, (f, pt) => pt.Value * 0.1m) // 10% tax
        .RuleFor(pt => pt.CreatedAt, f => f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now))
        .RuleFor(pt => pt.UpdatedAt, f => null)
        .RuleFor(pt => pt.IsDeleted, f => false);

    private static readonly string[] TraceNames =
    {
        "Property Sale",
        "Price Change",
        "Market Update",
        "Owner Transfer",
        "Valuation Update",
        "Property Update - Price Change",
        "Initial Property Creation",
        "Tax Assessment",
        "Market Adjustment",
        "Investment Update"
    };

    #region Basic Scenarios

    /// <summary>
    /// Creates a simple valid property trace
    /// </summary>
    public static PropertyTrace Simple()
    {
        return _faker.Generate();
    }

    /// <summary>
    /// Creates a property trace for specific property
    /// </summary>
    public static PropertyTrace ForProperty(int propertyId)
    {
        var trace = _faker.Generate();
        trace.IdProperty = propertyId;
        return trace;
    }

    /// <summary>
    /// Creates a property trace with property relationship
    /// </summary>
    public static PropertyTrace WithProperty()
    {
        var trace = _faker.Generate();
        trace.Property = PropertyMother.Simple();
        trace.Property.IdProperty = trace.IdProperty;
        return trace;
    }

    #endregion

    #region Transaction Type Scenarios

    /// <summary>
    /// Creates a property sale trace
    /// </summary>
    public static PropertyTrace PropertySale()
    {
        var trace = _faker.Generate();
        trace.Name = "Property Sale";
        trace.DateSale = DateTime.UtcNow.AddDays(-30);
        trace.Value = new Faker().Random.Decimal(100000m, 2000000m);
        trace.Tax = trace.Value * 0.1m;
        return trace;
    }

    /// <summary>
    /// Creates a price change trace
    /// </summary>
    public static PropertyTrace PriceChange()
    {
        var trace = _faker.Generate();
        trace.Name = "Property Update - Price Change";
        trace.DateSale = DateTime.UtcNow;
        trace.Value = new Faker().Random.Decimal(50000m, 1500000m);
        trace.Tax = trace.Value * 0.1m;
        return trace;
    }

    /// <summary>
    /// Creates a market update trace
    /// </summary>
    public static PropertyTrace MarketUpdate()
    {
        var trace = _faker.Generate();
        trace.Name = "Market Update";
        trace.DateSale = DateTime.UtcNow.AddDays(-15);
        trace.Value = new Faker().Random.Decimal(75000m, 1800000m);
        trace.Tax = trace.Value * 0.08m; // Different tax rate
        return trace;
    }

    /// <summary>
    /// Creates an owner transfer trace
    /// </summary>
    public static PropertyTrace OwnerTransfer()
    {
        var trace = _faker.Generate();
        trace.Name = "Owner Transfer";
        trace.DateSale = DateTime.UtcNow.AddDays(-60);
        trace.Value = new Faker().Random.Decimal(80000m, 2500000m);
        trace.Tax = trace.Value * 0.12m; // Higher tax for transfers
        return trace;
    }

    /// <summary>
    /// Creates a valuation update trace
    /// </summary>
    public static PropertyTrace ValuationUpdate()
    {
        var trace = _faker.Generate();
        trace.Name = "Valuation Update";
        trace.DateSale = DateTime.UtcNow.AddDays(-90);
        trace.Value = new Faker().Random.Decimal(60000m, 3000000m);
        trace.Tax = trace.Value * 0.05m; // Lower tax for valuations
        return trace;
    }

    /// <summary>
    /// Creates an initial property creation trace
    /// </summary>
    public static PropertyTrace InitialCreation()
    {
        var trace = _faker.Generate();
        trace.Name = "Initial Property Creation";
        trace.DateSale = DateTime.UtcNow.AddDays(-365);
        trace.Value = new Faker().Random.Decimal(100000m, 1000000m);
        trace.Tax = trace.Value * 0.1m;
        return trace;
    }

    #endregion

    #region Value-based Scenarios

    /// <summary>
    /// Creates a high-value property trace (luxury)
    /// </summary>
    public static PropertyTrace HighValue()
    {
        var trace = _faker.Generate();
        trace.Name = "Luxury Property Sale";
        trace.Value = new Faker().Random.Decimal(2000000m, 10000000m);
        trace.Tax = trace.Value * 0.15m; // Higher luxury tax
        return trace;
    }

    /// <summary>
    /// Creates a low-value property trace (budget)
    /// </summary>
    public static PropertyTrace LowValue()
    {
        var trace = _faker.Generate();
        trace.Name = "Budget Property Sale";
        trace.Value = new Faker().Random.Decimal(25000m, 100000m);
        trace.Tax = trace.Value * 0.08m; // Lower tax rate
        return trace;
    }

    /// <summary>
    /// Creates a medium-value property trace
    /// </summary>
    public static PropertyTrace MediumValue()
    {
        var trace = _faker.Generate();
        trace.Name = "Standard Property Sale";
        trace.Value = new Faker().Random.Decimal(200000m, 800000m);
        trace.Tax = trace.Value * 0.1m; // Standard tax rate
        return trace;
    }

    #endregion

    #region Time-based Scenarios

    /// <summary>
    /// Creates a recent property trace (within last month)
    /// </summary>
    public static PropertyTrace Recent()
    {
        var trace = _faker.Generate();
        trace.DateSale = DateTime.UtcNow.AddDays(-new Faker().Random.Int(1, 30));
        return trace;
    }

    /// <summary>
    /// Creates an old property trace (more than a year ago)
    /// </summary>
    public static PropertyTrace Old()
    {
        var trace = _faker.Generate();
        trace.DateSale = DateTime.UtcNow.AddYears(-new Faker().Random.Int(1, 5));
        return trace;
    }

    /// <summary>
    /// Creates a trace from today
    /// </summary>
    public static PropertyTrace Today()
    {
        var trace = _faker.Generate();
        trace.DateSale = DateTime.UtcNow.Date;
        return trace;
    }

    /// <summary>
    /// Creates a trace from this year
    /// </summary>
    public static PropertyTrace ThisYear()
    {
        var trace = _faker.Generate();
        trace.DateSale = new Faker().Date.Between(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now);
        return trace;
    }

    /// <summary>
    /// Creates a trace from last year
    /// </summary>
    public static PropertyTrace LastYear()
    {
        var trace = _faker.Generate();
        var lastYear = DateTime.Now.Year - 1;
        trace.DateSale = new Faker().Date.Between(new DateTime(lastYear, 1, 1), new DateTime(lastYear, 12, 31));
        return trace;
    }

    #endregion

    #region Validation Scenarios

    /// <summary>
    /// Creates a property trace with invalid empty name
    /// </summary>
    public static PropertyTrace WithInvalidName()
    {
        var trace = _faker.Generate();
        trace.Name = string.Empty;
        return trace;
    }

    /// <summary>
    /// Creates a property trace with name too long
    /// </summary>
    public static PropertyTrace WithLongName()
    {
        var trace = _faker.Generate();
        trace.Name = new string('a', 201); // Exceeds 200 character limit
        return trace;
    }

    /// <summary>
    /// Creates a property trace with invalid zero value
    /// </summary>
    public static PropertyTrace WithInvalidValue()
    {
        var trace = _faker.Generate();
        trace.Value = 0m;
        trace.Tax = 0m;
        return trace;
    }

    /// <summary>
    /// Creates a property trace with negative value
    /// </summary>
    public static PropertyTrace WithNegativeValue()
    {
        var trace = _faker.Generate();
        trace.Value = -50000m;
        trace.Tax = -5000m;
        return trace;
    }

    /// <summary>
    /// Creates a property trace with invalid property ID
    /// </summary>
    public static PropertyTrace WithInvalidPropertyId()
    {
        var trace = _faker.Generate();
        trace.IdProperty = -1; // Invalid ID
        return trace;
    }

    /// <summary>
    /// Creates a property trace with future date
    /// </summary>
    public static PropertyTrace WithFutureDate()
    {
        var trace = _faker.Generate();
        trace.DateSale = DateTime.UtcNow.AddDays(30); // Future date
        return trace;
    }

    /// <summary>
    /// Creates a property trace with inconsistent tax calculation
    /// </summary>
    public static PropertyTrace WithInconsistentTax()
    {
        var trace = _faker.Generate();
        trace.Value = 100000m;
        trace.Tax = 5000m; // Should be 10000m if 10% tax rate
        return trace;
    }

    #endregion

    #region State Scenarios

    /// <summary>
    /// Creates a soft-deleted property trace
    /// </summary>
    public static PropertyTrace Deleted()
    {
        var trace = _faker.Generate();
        trace.IsDeleted = true;
        return trace;
    }

    /// <summary>
    /// Creates a recently updated property trace
    /// </summary>
    public static PropertyTrace Updated()
    {
        var trace = _faker.Generate();
        trace.UpdatedAt = DateTime.UtcNow;
        return trace;
    }

    /// <summary>
    /// Creates a property trace created today
    /// </summary>
    public static PropertyTrace CreatedToday()
    {
        var trace = _faker.Generate();
        trace.CreatedAt = DateTime.UtcNow.Date;
        return trace;
    }

    #endregion

    #region Custom Scenarios

    /// <summary>
    /// Creates a property trace with specific ID
    /// </summary>
    public static PropertyTrace WithId(int id)
    {
        var trace = _faker.Generate();
        trace.IdPropertyTrace = id;
        return trace;
    }

    /// <summary>
    /// Creates a property trace with specific value and tax
    /// </summary>
    public static PropertyTrace WithValueAndTax(decimal value, decimal tax)
    {
        var trace = _faker.Generate();
        trace.Value = value;
        trace.Tax = tax;
        return trace;
    }

    /// <summary>
    /// Creates a property trace with specific date
    /// </summary>
    public static PropertyTrace WithDate(DateTime dateSale)
    {
        var trace = _faker.Generate();
        trace.DateSale = dateSale;
        return trace;
    }

    /// <summary>
    /// Creates a property trace with specific name
    /// </summary>
    public static PropertyTrace WithName(string name)
    {
        var trace = _faker.Generate();
        trace.Name = name;
        return trace;
    }

    /// <summary>
    /// Creates a property trace with standard 10% tax calculation
    /// </summary>
    public static PropertyTrace WithStandardTax(decimal value)
    {
        var trace = _faker.Generate();
        trace.Value = value;
        trace.Tax = value * 0.1m;
        return trace;
    }

    #endregion

    #region Lists

    /// <summary>
    /// Creates multiple property traces
    /// </summary>
    public static List<PropertyTrace> Multiple(int count = 3, int? propertyId = null)
    {
        var traces = _faker.Generate(count);
        if (propertyId.HasValue)
        {
            foreach (var trace in traces)
            {
                trace.IdProperty = propertyId.Value;
            }
        }
        return traces;
    }

    /// <summary>
    /// Creates a chronological sequence of property traces
    /// </summary>
    public static List<PropertyTrace> ChronologicalSequence(int propertyId, int count = 5)
    {
        var traces = new List<PropertyTrace>();
        var baseDate = DateTime.UtcNow.AddYears(-2);

        for (int i = 0; i < count; i++)
        {
            var trace = _faker.Generate();
            trace.IdProperty = propertyId;
            trace.DateSale = baseDate.AddMonths(i * 4); // Every 4 months
            trace.Value = 100000m + (i * 50000m); // Increasing value
            trace.Tax = trace.Value * 0.1m;
            trace.Name = i == 0 ? "Initial Property Creation" : $"Update {i}";
            traces.Add(trace);
        }

        return traces;
    }

    /// <summary>
    /// Creates property traces showing price evolution
    /// </summary>
    public static List<PropertyTrace> PriceEvolution(int propertyId)
    {
        var traces = new List<PropertyTrace>();

        // Initial creation
        var initialTrace = WithValueAndTax(200000m, 20000m);
        initialTrace.IdProperty = propertyId;
        initialTrace.Name = "Initial Property Creation";
        initialTrace.DateSale = DateTime.UtcNow.AddYears(-2);
        traces.Add(initialTrace);

        // Market appreciation
        var appreciationTrace = WithValueAndTax(250000m, 25000m);
        appreciationTrace.IdProperty = propertyId;
        appreciationTrace.Name = "Market Appreciation";
        appreciationTrace.DateSale = DateTime.UtcNow.AddYears(-1);
        traces.Add(appreciationTrace);

        // Property update
        var updateTrace = WithValueAndTax(300000m, 30000m);
        updateTrace.IdProperty = propertyId;
        updateTrace.Name = "Property Update - Price Change";
        updateTrace.DateSale = DateTime.UtcNow.AddMonths(-6);
        traces.Add(updateTrace);

        // Current market value
        var currentTrace = WithValueAndTax(350000m, 35000m);
        currentTrace.IdProperty = propertyId;
        currentTrace.Name = "Current Market Value";
        currentTrace.DateSale = DateTime.UtcNow;
        traces.Add(currentTrace);

        return traces;
    }

    /// <summary>
    /// Creates property traces with different transaction types
    /// </summary>
    public static List<PropertyTrace> DifferentTransactionTypes(int propertyId)
    {
        var traces = new List<PropertyTrace>();

        var propertySale = PropertySale();
        propertySale.IdProperty = propertyId;
        traces.Add(propertySale);

        var priceChange = PriceChange();
        priceChange.IdProperty = propertyId;
        traces.Add(priceChange);

        var marketUpdate = MarketUpdate();
        marketUpdate.IdProperty = propertyId;
        traces.Add(marketUpdate);

        var ownerTransfer = OwnerTransfer();
        ownerTransfer.IdProperty = propertyId;
        traces.Add(ownerTransfer);

        var valuationUpdate = ValuationUpdate();
        valuationUpdate.IdProperty = propertyId;
        traces.Add(valuationUpdate);

        return traces;
    }

    /// <summary>
    /// Creates property traces for multiple properties
    /// </summary>
    public static List<PropertyTrace> ForMultipleProperties(List<int> propertyIds, int tracesPerProperty = 2)
    {
        var traces = new List<PropertyTrace>();

        foreach (var propertyId in propertyIds)
        {
            traces.AddRange(Multiple(tracesPerProperty, propertyId));
        }

        return traces;
    }

    /// <summary>
    /// Creates property traces for testing date ranges
    /// </summary>
    public static List<PropertyTrace> DateRangeScenarios(int propertyId)
    {
        var traces = new List<PropertyTrace>();

        var todayTrace = Today();
        todayTrace.IdProperty = propertyId;
        traces.Add(todayTrace);

        var recentTrace = Recent();
        recentTrace.IdProperty = propertyId;
        traces.Add(recentTrace);

        var thisYearTrace = ThisYear();
        thisYearTrace.IdProperty = propertyId;
        traces.Add(thisYearTrace);

        var lastYearTrace = LastYear();
        lastYearTrace.IdProperty = propertyId;
        traces.Add(lastYearTrace);

        var oldTrace = Old();
        oldTrace.IdProperty = propertyId;
        traces.Add(oldTrace);

        return traces;
    }

    /// <summary>
    /// Creates property traces for testing value ranges
    /// </summary>
    public static List<PropertyTrace> ValueRangeScenarios(int propertyId)
    {
        var traces = new List<PropertyTrace>();

        var lowValue = LowValue();
        lowValue.IdProperty = propertyId;
        traces.Add(lowValue);

        var mediumValue = MediumValue();
        mediumValue.IdProperty = propertyId;
        traces.Add(mediumValue);

        var highValue = HighValue();
        highValue.IdProperty = propertyId;
        traces.Add(highValue);

        return traces;
    }

    /// <summary>
    /// Creates property traces for pagination testing
    /// </summary>
    public static List<PropertyTrace> ForPagination(int propertyId, int totalCount = 20)
    {
        var traces = new List<PropertyTrace>();
        for (int i = 1; i <= totalCount; i++)
        {
            var trace = _faker.Generate();
            trace.IdPropertyTrace = i;
            trace.IdProperty = propertyId;
            trace.Name = $"Transaction {i:D2}";
            trace.DateSale = DateTime.UtcNow.AddDays(-i);
            traces.Add(trace);
        }
        return traces.OrderByDescending(t => t.DateSale).ToList();
    }

    #endregion
}