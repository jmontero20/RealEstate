﻿namespace RealEstate.Application.Common.Interfaces
{
    public interface IQuery<out TResponse>
    {
    }

    public interface IQueryHandler<in TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
