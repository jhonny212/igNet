using Domain.Entities;
using Domain.Interfaces;
using IgNet.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public partial class Repository<T, PK> : BaseRepository<T, PK>, IRepository<T, PK>
        where T : BaseEntity<PK>
    {
        public Repository(DbContext context)
            : base(context, context.Set<T>()) { }
    }
}
