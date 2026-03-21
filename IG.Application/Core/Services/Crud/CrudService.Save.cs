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
        protected const string CREATED_MESSAGE = "Registro guardado correctamente";
        protected const string UPDATED_MESSAGE = "Registro actualizado correctamente";
        protected const string DELETED_MESSAGE = "Registro eliminado correctamente";

        public virtual async Task<(Response<CreateRes>, TEntity?)> CreateAsync(CreateReq request)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(request);
                var result = await _repository.CreateAsync(entity, request.LoggedInUser);

                return (
                    new Response<CreateRes>
                    {
                        Success = true,
                        Message = CREATED_MESSAGE,
                        Data = this.MapToCreateRes<CreateRes>(result),
                    },
                    result
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                //return (
                //    new Response<CreateRes>
                //    {
                //        Success = false,
                //        Message = "Error al crear registro",
                //        ServerMessage = ex.Message,
                //    },
                //    null
                //);
            }
        }

        public virtual async Task<(Response<UpdateRes>, TEntity?)> UpdateAsync(
            PK id,
            UpdateReq request
        )
        {
            try
            {
                TEntity? entity = await _repository.FindByIdAsync(id);
                if (entity == null)
                {
                    return (
                        new Response<UpdateRes>
                        {
                            Success = false,
                            Message = "Registro no encontrado",
                        },
                        null
                    );
                }
                _mapper.Map(request, entity);
                entity.Id = id;

                await _repository.UpdateAsync(entity, request.LoggedInUser);

                return (
                    new Response<UpdateRes>
                    {
                        Success = true,
                        Message = UPDATED_MESSAGE,
                        Data = this.MapToCreateRes<UpdateRes>(entity),
                    },
                    entity
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                //return (
                //    new Response<UpdateRes>
                //    {
                //        Success = false,
                //        Message = "Error al actualizar",
                //        ServerMessage = ex.Message,
                //    },
                //    null
                //);
            }
        }

        private SaveRes? MapToCreateRes<SaveRes>(TEntity entity)
        {
            SaveRes? responseData = default;
            if (typeof(SaveRes) == typeof(string))
            {
                responseData = (SaveRes?)(object?)entity?.Id?.ToString();
            }
            else
            {
                responseData = _mapper.Map<SaveRes>(entity);
            }
            return responseData;
        }
    }
}
