using RealEstate.Domain.Comon;

namespace RealEstate.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IPropertyRepository Properties { get; }
        IPropertyImageRepository PropertyImages { get; }
        IPropertyTraceRepository PropertyTraces { get; }  
        IOwnerRepository Owners { get; }

        Task<Result> SaveChangesAsync(CancellationToken cancellationToken);
        Task<Result> BeginTransactionAsync(CancellationToken cancellationToken);      
        Task<Result> CommitTransactionAsync(CancellationToken cancellationToken);    
        Task<Result> RollbackTransactionAsync(CancellationToken cancellationToken);  
    }
}
