using Microsoft.EntityFrameworkCore.Storage;
using RealEstate.Domain.Comon;
using RealEstate.Domain.Contracts;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly RealEstateDbContext _context;
        private IDbContextTransaction? _transaction;

        private IPropertyRepository? _properties;
        private IPropertyImageRepository? _propertyImages;
        private IPropertyTraceRepository? _propertyTraces;
        private IOwnerRepository? _owners;

        public UnitOfWork(RealEstateDbContext context)
        {
            _context = context;
        }

        public IPropertyRepository Properties =>
            _properties ??= new PropertyRepository(_context);

        public IPropertyImageRepository PropertyImages =>
            _propertyImages ??= new PropertyImageRepository(_context);

        public IPropertyTraceRepository PropertyTraces =>
            _propertyTraces ??= new PropertyTraceRepository(_context);

        public IOwnerRepository Owners =>
            _owners ??= new OwnerRepository(_context);

        public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error saving changes: {ex.Message}");
            }
        }

        public async Task<Result> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error beginning transaction: {ex.Message}");
            }
        }

        public async Task<Result> CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_transaction == null)
                    return Result.Failure("No active transaction to commit");

                await _transaction.CommitAsync(cancellationToken);
                await _transaction.DisposeAsync();
                _transaction = null;
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error committing transaction: {ex.Message}");
            }
        }

        public async Task<Result> RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_transaction == null)
                    return Result.Failure("No active transaction to rollback");

                await _transaction.RollbackAsync(cancellationToken);
                await _transaction.DisposeAsync();
                _transaction = null;
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error rolling back transaction: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
