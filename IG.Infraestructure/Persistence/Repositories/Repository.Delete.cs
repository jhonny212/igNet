using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public partial class Repository<T, PK>
        where T : BaseEntity<PK>
    {
        public async Task<T> DeleteAsync(
            T entity,
            bool softDelete = false,
            string deletedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        )
        {
            if (softDelete)
            {
                entity.DeletedAt = DateTime.UtcNow;
                entity.DeletedBy = deletedBy;
                entity.IsDeleted = true;
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<int> BulkDeleteAsync(
            IEnumerable<T> entities,
            bool softDelete = false,
            string deletedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        )
        {
            var now = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                if (softDelete)
                {
                    entity.DeletedAt = now;
                    entity.DeletedBy = deletedBy;
                    entity.IsDeleted = true;
                }
                else
                {
                    _dbSet.Remove(entity);
                }
            }

            _dbSet.UpdateRange(entities);

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteByIdAsync(
            PK id,
            bool softDelete = false,
            string deletedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        )
        {
            if (softDelete)
            {
                return await _dbSet
                    .Where(e => e.Id!.Equals(id))
                    .ExecuteUpdateAsync(
                        s =>
                            s.SetProperty(p => p.DeletedAt, DateTime.UtcNow)
                                .SetProperty(p => p.DeletedBy, deletedBy),
                        cancellationToken
                    );
            }
            else
            {
                var entity = await _dbSet.FindAsync(new object[] { id! }, cancellationToken);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    return await _context.SaveChangesAsync(cancellationToken);
                }
                return 0;
            }
        }

        public async Task<int> DeleteByIdListAsync(
            IEnumerable<PK> ids,
            bool softDelete = false,
            string deletedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        )
        {
            if (softDelete)
            {
                return await _dbSet
                    .Where(e => ids.Contains(e.Id))
                    .ExecuteUpdateAsync(
                        s =>
                            s.SetProperty(p => p.DeletedAt, DateTime.UtcNow)
                                .SetProperty(p => p.DeletedBy, deletedBy),
                        cancellationToken
                    );
            }
            else
            {
                var entities = await _dbSet
                    .Where(e => ids.Contains(e.Id))
                    .ToListAsync(cancellationToken);
                if (entities.Any())
                {
                    _dbSet.RemoveRange(entities);
                    return await _context.SaveChangesAsync(cancellationToken);
                }
                return 0;
            }
        }

        public async Task<int> DeleteWhereAsync(
            Expression<Func<T, bool>> predicate,
            bool softDelete = false,
            string deletedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        )
        {
            if (softDelete)
            {
                return await _dbSet
                    .Where(predicate)
                    .ExecuteUpdateAsync(
                        s =>
                            s.SetProperty(p => p.DeletedAt, DateTime.UtcNow)
                                .SetProperty(p => p.DeletedBy, deletedBy),
                        cancellationToken
                    );
            }
            else
            {
                var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
                if (entities.Any())
                {
                    _dbSet.RemoveRange(entities);
                    return await _context.SaveChangesAsync(cancellationToken);
                }
            }
            return 0;
        }
    }
}
