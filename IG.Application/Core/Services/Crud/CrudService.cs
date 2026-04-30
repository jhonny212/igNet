using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Response;
using IG.Application.Core.Attributes;
using IG.Application.Core.Extensions;
using IG.Application.Core.Interfaces.Crud;
using IG.Application.Domain.Interfaces;
using System.Reflection;
using IG.Application.Core.Interfaces;

namespace IG.Application.Services.Crud
{
    public partial class CrudService<TEntity, PK, CreateReq, CreateRes, UpdateReq, UpdateRes>
        : ICrudService<TEntity, PK, CreateReq, CreateRes, UpdateReq, UpdateRes>
        where CreateReq : IBaseRequest
        where UpdateReq : IBaseRequest
        where TEntity : class, IBaseEntity<PK>
    {
        protected const string CREATE_OPERATION = "CREATE";
        protected const string UPDATE_OPERATION = "UPDATE";
        protected const string DELETE_OPERATION = "DELETE";

        protected readonly IRepository<TEntity, PK> _repository;
        protected readonly IMapper _mapper;

        public CrudService(IRepository<TEntity, PK> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        protected virtual string GetEntityDisplayName()
        {
            var entityType = typeof(TEntity);
            var entityAttribute = entityType.GetCustomAttribute<EntityDisplayNameAttribute>();

            var entityName = entityAttribute?.Name;
            if (string.IsNullOrWhiteSpace(entityName))
            {
                entityName = entityType.Name;
                if (entityName.EndsWith("Entity", StringComparison.OrdinalIgnoreCase))
                {
                    entityName = entityName[..^"Entity".Length];
                }
            }

            return entityName.UppercaseFirstWord();
        }

        protected virtual string BuildCreatedMessage()
        {
            return $"{GetEntityDisplayName()} guardado correctamente";
        }

        protected virtual string BuildUpdatedMessage()
        {
            return $"{GetEntityDisplayName()} actualizado correctamente";
        }

        protected virtual string BuildDeletedMessage()
        {
            return $"{GetEntityDisplayName()} eliminado correctamente";
        }

        protected virtual string BuildNotFoundMessage()
        {
            return $"{GetEntityDisplayName()} no encontrado";
        }
    }
}
