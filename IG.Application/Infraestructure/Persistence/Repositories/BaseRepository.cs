using IG.Application.Domain.Entities;
using IG.Application.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IG.Application.Infraestructure.Persistence.Repositories
{
    public class BaseRepository<T, PK> : IBaseRepository
        where T : BaseEntity<PK>
    {
        public readonly DbContext _context;
        public readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken = default
        )
        {
            return await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(
            IDbContextTransaction transaction,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task RollbackTransactionAsync(
            IDbContextTransaction transaction,
            CancellationToken cancellationToken = default
        )
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }
}
