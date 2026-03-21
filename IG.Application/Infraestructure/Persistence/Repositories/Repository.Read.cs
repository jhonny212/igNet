using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IG.Application.Core.Interfaces;
using IG.Application.Domain.Interfaces;
using IG.Application.Infraestructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IG.Application.Infraestructure.Persistence.Repositories
{
    public partial class Repository<T, PK>
        where T : class, IBaseEntity<PK>
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

        public IQueryable<T> Search(
            out int totalPages,
            out int totalRecords,
            string? searchTerm,
            bool tracking = false,
            IPagedAndSort? paged = null,
            IEnumerable<Expression<Func<T, object>>>? includes = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, string>>[] columns
        )
        {
            totalRecords = 0;
            totalPages = 0;
            var query = this._dbSet.AsQueryable().WhereSearch(searchTerm, columns);
            if (!tracking)
                query = query.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            if (paged != null)
            {
                query = query.ToPageAndSort(paged, out totalRecords, out totalPages);
            }
            return query.AsQueryable();
        }
    }
}
