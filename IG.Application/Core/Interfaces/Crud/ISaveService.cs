using Core.Response;
using IG.Application.Core.Request;
using IG.Application.Domain.Interfaces;

namespace IG.Application.Core.Interfaces.Crud
{
    public interface ISaveService<TEntity, PK, CreateReq, CreateRes, UpdateReq, UpdateRes>
        where CreateReq : BaseRequest
        where UpdateReq : BaseRequest
        where TEntity : IBaseEntity<PK>
    {
        Task<(Response<CreateRes>, TEntity?)> CreateAsync(CreateReq request);
        Task<(Response<UpdateRes>, TEntity?)> UpdateAsync(PK id, UpdateReq request);
    }
}
