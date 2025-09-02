using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Common.Services
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
            var handler = _serviceProvider.GetRequiredService(handlerType);

            var method = handlerType.GetMethod("Handle");
            if (method == null)
                throw new InvalidOperationException($"Handler method not found for {query.GetType().Name}");

            var result = method.Invoke(handler, new object[] { query, cancellationToken });

            if (result is Task<TResponse> task)
                return await task;

            throw new InvalidOperationException($"Handler for {query.GetType().Name} did not return expected type");
        }

        public async Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
            var handler = _serviceProvider.GetRequiredService(handlerType);

            var method = handlerType.GetMethod("Handle");
            if (method == null)
                throw new InvalidOperationException($"Handler method not found for {command.GetType().Name}");

            var result = method.Invoke(handler, new object[] { command, cancellationToken });

            if (result is Task<TResponse> task)
                return await task;

            throw new InvalidOperationException($"Handler for {command.GetType().Name} did not return expected type");
        }
    }
}
