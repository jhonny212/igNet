using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Response;
using IG.Application.Core.Exceptions;
using IG.Application.Core.Interfaces;
using IG.Application.Domain.Interfaces;

namespace IG.Application.Services.Crud
{
    public partial class CrudService<TEntity, PK, CreateReq, CreateRes, UpdateReq, UpdateRes>
        where CreateReq : IBaseRequest
        where UpdateReq : IBaseRequest
        where TEntity : class, IBaseEntity<PK>
    {
        public virtual async Task<Response<bool>> DeleteAsync(
            PK id,
            bool safeDelete = false,
            string deletedBy = "SYSTEM"
        )
        {
            try
            {
                await _repository.DeleteByIdAsync(id, safeDelete, deletedBy);
                return new Response<bool>
                {
                    Success = true,
                    Message = BuildDeletedMessage(),
                    Data = true,
                };
            }
            catch (Exception ex)
            {
                if (ex is ExceptionIg)
                {
                    throw;
                }

                throw new ExceptionIg(
                    message: $"Error al eliminar {GetEntityDisplayName().ToLowerInvariant()}",
                    entityName: GetEntityDisplayName(),
                    operation: DELETE_OPERATION,
                    innerException: ex
                );
            }
        }
    }
}
