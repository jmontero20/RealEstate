using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Domain.Contracts;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Services;


namespace RealEstate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add SQLite Database
            services.AddDbContext<RealEstateDbContext>(options =>
                options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(RealEstateDbContext).Assembly.FullName)));

            // Add Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            // Add Azure Blob Storage
            var blobConnectionString = configuration.GetConnectionString("BlobStorage");
            if (!string.IsNullOrEmpty(blobConnectionString))
            {
                services.AddSingleton(x => new BlobServiceClient(blobConnectionString));
                services.AddScoped<IBlobStorageService>(provider =>
                {
                    var blobServiceClient = provider.GetRequiredService<BlobServiceClient>();
                    var containerName = configuration["BlobStorage:ContainerName"] ?? "property-images";
                    return new AzureBlobStorageService(blobServiceClient, containerName);
                });
            }

            return services;
        }

        public static async Task<IServiceProvider> MigrateAndSeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<RealEstateDbContext>();

            // Ensure database is created and migrated
            await context.Database.EnsureCreatedAsync();

            return serviceProvider;
        }
    }
}
