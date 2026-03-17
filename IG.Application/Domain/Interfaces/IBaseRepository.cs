using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace IG.Application.Domain.Interfaces
{
    public interface IBaseRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken = default
        );

        Task CommitTransactionAsync(
            IDbContextTransaction transaction,
            CancellationToken cancellationToken = default
        );

        Task RollbackTransactionAsync(
            IDbContextTransaction transaction,
            CancellationToken cancellationToken = default
        );
    }
}
