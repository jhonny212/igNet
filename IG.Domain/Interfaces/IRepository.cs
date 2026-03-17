using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRepository<T, PK>
        : ISaveRepository<T, PK>,
            IDeleteRepository<T, PK>,
            IReadRepository<T, PK>,
            IBaseRepository
        where T : BaseEntity<PK> { }
}
//IDeleteRepository<T, PK>,
//IReadRepository<T, PK>,
//IBaseRepository
