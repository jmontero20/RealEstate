using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Common.Interfaces;
using RealEstate.SharedKernel.Result;

namespace RealEstateAPI.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Buscar comandos y queries en los argumentos del action
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null) continue;

                var argumentType = argument.GetType();

                // Verificar si es un Command o Query
                if (IsCommandOrQuery(argumentType))
                {
                    var validationResult = await ValidateAsync(argument, argumentType);
                    if (!validationResult.IsValid)
                    {
                        var errorResponse = CreateValidationErrorResponse(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
                        context.Result = new BadRequestObjectResult(errorResponse);
                        return;
                    }
                }
            }

            // Si todas las validaciones pasan, continuar con la ejecución
            await next();
        }

        private static bool IsCommandOrQuery(Type type)
        {
            var interfaces = type.GetInterfaces();
            return interfaces.Any(i =>
                (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommand<>)) ||
                (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuery<>)));
        }

        private async Task<FluentValidation.Results.ValidationResult> ValidateAsync(object instance, Type instanceType)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(instanceType);

            var validator = _serviceProvider.GetService(validatorType) as IValidator;
            if (validator == null)
            {
                // Si no hay validator, consideramos que es válido
                return new FluentValidation.Results.ValidationResult();
            }

            var context = new ValidationContext<object>(instance);
            return await validator.ValidateAsync(context);
        }

        private static Result<object> CreateValidationErrorResponse(List<string> errors)
        {
            return Result<object>.Failure(errors, "Validation failed");
        }
    }
}
