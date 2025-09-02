using RealEstate.Domain.Entities;


namespace RealEstate.Infrastructure.Data.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(RealEstateDbContext context)
        {
            if (!context.Owners.Any())
            {
                var owners = new List<Owner>
            {
                new Owner
                {
                    IdOwner = 1,
                    Name = "John Smith",
                    Address = "123 Main Street, Miami, FL 33101",
                    Birthday = new DateTime(1980, 5, 15),
                    Photo = null
                },
                new Owner
                {
                    IdOwner = 2,
                    Name = "Sarah Johnson",
                    Address = "456 Oak Avenue, New York, NY 10001",
                    Birthday = new DateTime(1975, 11, 22),
                    Photo = null
                },
                new Owner
                {
                    IdOwner = 3,
                    Name = "Michael Brown",
                    Address = "789 Pine Road, Los Angeles, CA 90210",
                    Birthday = new DateTime(1990, 3, 8),
                    Photo = null
                }
            };

                context.Owners.AddRange(owners);
                await context.SaveChangesAsync();
            }

            if (!context.Properties.Any())
            {
                var properties = new List<Property>
            {
                new Property
                {
                    IdProperty = 1,
                    Name = "Luxury Apartment Downtown",
                    Address = "100 Biscayne Blvd, Miami, FL 33132",
                    Price = 750000.00m,
                    CodeInternal = "MIA001",
                    Year = 2020,
                    IdOwner = 1
                },
                new Property
                {
                    IdProperty = 2,
                    Name = "Modern Condo Manhattan",
                    Address = "200 Central Park West, New York, NY 10024",
                    Price = 1250000.00m,
                    CodeInternal = "NYC001",
                    Year = 2018,
                    IdOwner = 2
                },
                new Property
                {
                    IdProperty = 3,
                    Name = "Beverly Hills Villa",
                    Address = "300 Rodeo Drive, Beverly Hills, CA 90210",
                    Price = 3500000.00m,
                    CodeInternal = "LA001",
                    Year = 2015,
                    IdOwner = 3
                }
            };

                context.Properties.AddRange(properties);
                await context.SaveChangesAsync();
            }
        }
    }
}
