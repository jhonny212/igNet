using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReadRepository<T, PK>
        where T : BaseEntity<PK>
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

        Task<List<T>> Search(
            string? searchTerm,
            bool tracking = false,
            IEnumerable<Expression<Func<T, object>>>? includes = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, string>>[] columns
        );
    }
}
