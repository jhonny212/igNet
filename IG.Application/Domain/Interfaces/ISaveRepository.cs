using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace IG.Application.Domain.Interfaces
{
    public interface ISaveRepository<T, PK>
        where T : IBaseEntity<PK>
    {
        Task<T> CreateAsync(
            T entity,
            string createdBy = "SYSTEM",
            CancellationToken cancellationToken = default
        );

        Task<IEnumerable<T>> CreateBulkAsync(
            IEnumerable<T> entities,
            string createdBy = "SYSTEM",
            CancellationToken cancellationToken = default
        );

        Task<T> UpdateAsync(
            T entity,
            string updatedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        );

        Task<IEnumerable<T>> UpdateBulkAsync(
            IEnumerable<T> entities,
            string updatedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        );

        Task<int> PatchAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls,
            string updatedBy = "SYSTEM",
            CancellationToken cancellationToken = default
        );
    }
}
