using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDeleteRepository<T, PK>
        where T : BaseEntity<PK>
    {
        Task<T> DeleteAsync(
            T entity,
            bool softDelete = false,
            string deletedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        );

        Task<int> DeleteByIdAsync(
            PK id,
            bool softDelete = false,
            string deletedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        );

        Task<int> DeleteByIdListAsync(
            IEnumerable<PK> ids,
            bool softDelete = false,
            string deletedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        );

        Task<int> BulkDeleteAsync(
            IEnumerable<T> entities,
            bool softDelete = false,
            string deletedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        );

        Task<int> DeleteWhereAsync(
            Expression<Func<T, bool>> predicate,
            bool softDelete = false,
            string deletedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        );
    }
}
