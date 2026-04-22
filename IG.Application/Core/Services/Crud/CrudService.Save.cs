using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Response;
using IG.Application.Core.Exceptions;
using IG.Application.Core.Request;
using IG.Application.Domain.Interfaces;

namespace IG.Application.Services.Crud
{
    public partial class CrudService<TEntity, PK, CreateReq, CreateRes, UpdateReq, UpdateRes>
        where CreateReq : BaseRequest
        where UpdateReq : BaseRequest
        where TEntity : class, IBaseEntity<PK>
    {
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
                        Message = BuildCreatedMessage(),
                        Data = this.MapToCreateRes<CreateRes>(result),
                    },
                    result
                );
            }
            catch (Exception ex)
            {
                if (ex is ExceptionIg)
                {
                    throw;
                }

                throw new ExceptionIg(
                    message: $"Error al guardar {GetEntityDisplayName().ToLowerInvariant()}",
                    entityName: GetEntityDisplayName(),
                    operation: CREATE_OPERATION,
                    innerException: ex
                );
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
                            Message = BuildNotFoundMessage(),
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
                        Message = BuildUpdatedMessage(),
                        Data = this.MapToCreateRes<UpdateRes>(entity),
                    },
                    entity
                );
            }
            catch (Exception ex)
            {
                if (ex is ExceptionIg)
                {
                    throw;
                }

                throw new ExceptionIg(
                    message: $"Error al actualizar {GetEntityDisplayName().ToLowerInvariant()}",
                    entityName: GetEntityDisplayName(),
                    operation: UPDATE_OPERATION,
                    innerException: ex
                );
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
