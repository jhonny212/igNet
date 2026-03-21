using IG.Application.Domain.Interfaces;
using IG.Application.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IG.Application.Infraestructure.Persistence.Repositories
{
    public partial class Repository<T, PK> : BaseRepository<T, PK>, IRepository<T, PK>
        where T : class, IBaseEntity<PK>
    {
        public Repository(DbContext context)
            : base(context) { }
    }
}
