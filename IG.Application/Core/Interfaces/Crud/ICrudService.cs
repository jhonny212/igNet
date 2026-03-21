using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IG.Application.Core.Request;
using IG.Application.Domain.Interfaces;

namespace IG.Application.Core.Interfaces.Crud
{
    public interface ICrudService<TEntity, PK, CreateReq, CreateRes, UpdateReq, UpdateRes>
        : ISaveService<TEntity, PK, CreateReq, CreateRes, UpdateReq, UpdateRes>,
            IDeleteService<PK>
        where CreateReq : BaseRequest
        where UpdateReq : BaseRequest
        where TEntity : class, IBaseEntity<PK> { }
}
