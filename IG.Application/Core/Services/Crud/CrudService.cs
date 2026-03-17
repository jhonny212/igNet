using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Response;
using IG.Application.Core.Interfaces.Crud;
using IG.Application.Core.Request;
using IG.Application.Domain.Entities;
using IG.Application.Domain.Interfaces;

namespace IG.Application.Services.Crud
{
    public partial class CrudService<TEntity, PK, CreateReq, CreateRes, UpdateReq, UpdateRes>
        : ICrudService<TEntity, PK, CreateReq, CreateRes, UpdateReq, UpdateRes>
        where CreateReq : BaseRequest
        where UpdateReq : BaseRequest
        where TEntity : BaseEntity<PK>
    {
        protected readonly IRepository<TEntity, PK> _repository;
        protected readonly IMapper _mapper;

        public CrudService(IRepository<TEntity, PK> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
    }
}
