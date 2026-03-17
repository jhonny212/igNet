using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IG.Application.Domain.Entities;
using IG.Application.Domain.Interfaces;
using IG.Application.Infraestructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IG.Application.Infraestructure.Persistence.Repositories
{
    public partial class Repository<T, PK>
        where T : BaseEntity<PK>
    {
        public async Task<T?> FindByIdAsync(
            PK id,
            bool tracking = false,
            IEnumerable<Expression<Func<T, object>>>? includes = null,
            CancellationToken cancellationToken = default
        )
        {
            IQueryable<T> query = _dbSet;

            if (!tracking)
                query = query.AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(x => x.Id!.Equals(id), cancellationToken);
        }

        public async Task<List<T>> FindByIdListAsync(
            IEnumerable<PK> ids,
            bool tracking = false,
            IEnumerable<Expression<Func<T, object>>>? includes = null,
            CancellationToken cancellationToken = default
        )
        {
            IQueryable<T> query = _dbSet;

            if (!tracking)
                query = query.AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
        }

        public async Task<List<T>> Search(
            string? searchTerm,
            bool tracking = false,
            IEnumerable<Expression<Func<T, object>>>? includes = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, string>>[] columns
        )
        {
            var query = this._dbSet.AsQueryable().WhereSearch(searchTerm, columns);
            if (!tracking)
                query = query.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            return await query.ToListAsync(cancellationToken);
        }
    }
}
