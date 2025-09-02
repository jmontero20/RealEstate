namespace RealEstate.Application.Common.Interfaces
{
    public interface ICommand<out TResponse>
    {
    }

    public interface ICommandHandler<in TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
