using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Common.Interfaces;
using RealEstate.Application.Common.Models;
using RealEstate.Application.Common.Services;
using RealEstate.Application.UsecCases.Property.Commands.CreateProperty;
using RealEstate.Application.UsecCases.Property.Commands.UpdateProperty;
using RealEstate.Application.UsecCases.Property.Commands.UpdatePropertyPrice;
using RealEstate.Application.UsecCases.Property.Queries.GetPropertiesWithFilters;
using RealEstate.Application.UsecCases.PropertyImages.Commands.AddPropertyImage;
using System.Reflection;


namespace RealEstate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register Mediator
            services.AddScoped<IMediator, Mediator>();

            // Register Property Command Handlers
            services.AddScoped<ICommandHandler<CreatePropertyCommand, ApplicationResponse<CreatePropertyResponse>>, CreatePropertyCommandHandler>();
            services.AddScoped<ICommandHandler<UpdatePropertyCommand, ApplicationResponse<UpdatePropertyResponse>>, UpdatePropertyCommandHandler>();
            services.AddScoped<ICommandHandler<UpdatePropertyPriceCommand, ApplicationResponse<UpdatePropertyPriceResponse>>, UpdatePropertyPriceCommandHandler>();

            // Register Property Query Handlers
            services.AddScoped<IQueryHandler<GetPropertiesWithFiltersQuery, ApplicationResponse<IEnumerable<GetPropertiesWithFiltersResponse>>>, GetPropertiesWithFiltersQueryHandler>();

            // Register PropertyImage Command Handlers
            services.AddScoped<ICommandHandler<AddPropertyImageCommand, ApplicationResponse<AddPropertyImageResponse>>, AddPropertyImageCommandHandler>();

            // Register FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register specific validators (opcional para mayor claridad)
            services.AddScoped<IValidator<CreatePropertyCommand>, CreatePropertyCommandValidator>();
            services.AddScoped<IValidator<UpdatePropertyCommand>, UpdatePropertyCommandValidator>();
            services.AddScoped<IValidator<UpdatePropertyPriceCommand>, UpdatePropertyPriceCommandValidator>();
            services.AddScoped<IValidator<GetPropertiesWithFiltersQuery>, GetPropertiesWithFiltersQueryValidator>();
            services.AddScoped<IValidator<AddPropertyImageCommand>, AddPropertyImageCommandValidator>();

            return services;
        }
    }
}
