using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Response;
using IG.Application.Core.Request;
using IG.Application.Domain.Interfaces;

namespace IG.Application.Services.Crud
{
    public partial class CrudService<TEntity, PK, CreateReq, CreateRes, UpdateReq, UpdateRes>
        where CreateReq : BaseRequest
        where UpdateReq : BaseRequest
        where TEntity : class, IBaseEntity<PK>
    {
        public async Task<Response<bool>> DeleteAsync(
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
                    Message = DELETED_MESSAGE,
                    Data = true,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                //return new Response<bool>
                //{
                //    Success = false,
                //    Message = "Error al eliminar",
                //    ServerMessage = ex.Message,
                //    Data = false,
                //};
            }
        }
    }
}
