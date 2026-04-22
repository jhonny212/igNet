using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IG.Application.Core.Interfaces;

namespace IG.Application.Domain.Interfaces
{
    public interface IReadRepository<T, PK>
        where T : IBaseEntity<PK>
    {
        Task<T?> FindByIdAsync(
            PK id,
            bool tracking = false,
            IEnumerable<Expression<Func<T, object>>>? includes = null,
            CancellationToken cancellationToken = default
        );

        Task<List<T>> FindByIdListAsync(
            IEnumerable<PK> ids,
            bool tracking = false,
            IEnumerable<Expression<Func<T, object>>>? includes = null,
            CancellationToken cancellationToken = default
        );

        //Task<List<T>> Search(
        //    Expression<Func<T, bool>>? predicate = null,
        //    bool tracking = false,
        //    IEnumerable<Expression<Func<T, object>>>? includes = null,
        //    CancellationToken cancellationToken = default
        //);

        IQueryable<T> Search(
            out int totalPages,
            out int totalRecords,
            string? searchTerm,
            bool tracking = false,
            IPagedAndSort? paged = null,
            IEnumerable<Expression<Func<T, object>>>? includes = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, string?>>[] columns
        );
    }
}
