using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IG.Application.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace IG.Application.Infraestructure.Persistence.Repositories
{
    public partial class Repository<T, PK>
        where T : class, IBaseEntity<PK>
    {
        public async Task<T> CreateAsync(
            T entity,
            string createdBy = "SYSTEM",
            CancellationToken cancellationToken = default
        )
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = createdBy;

            await _dbSet.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<T>> CreateBulkAsync(
            IEnumerable<T> entities,
            string createdBy = "SYSTEM",
            CancellationToken cancellationToken = default
        )
        {
            var now = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                entity.CreatedAt = now;
                entity.CreatedBy = createdBy;
            }

            await _dbSet.AddRangeAsync(entities, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entities;
        }

        public async Task<T> UpdateAsync(
            T entity,
            string updatedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        )
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = updatedBy;
            _dbSet.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<T>> UpdateBulkAsync(
            IEnumerable<T> entities,
            string updatedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        )
        {
            var now = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                entity.UpdatedAt = now;
                entity.UpdatedBy = updatedBy;
            }

            _dbSet.UpdateRange(entities);

            await _context.SaveChangesAsync(cancellationToken);

            return entities;
        }

        public async Task<int> PatchAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls,
            string updatedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        )
        {
            return await _dbSet
                .Where(predicate)
                .ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
            // return await _dbSet
            //     .Where(predicate)
            //     .ExecuteUpdateAsync(
            //         s =>
            //             setPropertyCalls
            //                 .Invoke(s)
            //                 .SetProperty(e => e.UpdatedAt, DateTime.UtcNow)
            //                 .SetProperty(e => e.UpdatedBy, updatedBy),
            //         cancellationToken
            //     );
        }
    }
}
