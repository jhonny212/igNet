using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IG.Application.Core.Extensions;
using IG.Application.Core.Interfaces;
using IG.Application.Core.Interfaces.Crud;
using IG.Application.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IG.Application.App.Controllers
{
    [ApiController]
    public abstract class BaseCrudController<
        TEntity,
        PK,
        CreateReq,
        CreateRes,
        UpdateReq,
        UpdateRes
    > : ControllerBase
        where CreateReq : IBaseRequest
        where UpdateReq : IBaseRequest
        where TEntity : class, IBaseEntity<PK>
    {
        protected readonly ICrudService<
            TEntity,
            PK,
            CreateReq,
            CreateRes,
            UpdateReq,
            UpdateRes
        > service;

        public BaseCrudController(
            ICrudService<TEntity, PK, CreateReq, CreateRes, UpdateReq, UpdateRes> service
        )
        {
            this.service = service;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] CreateReq request)
        {
            var result = await service.CreateAsync(request);
            return this.HandleResult(result.Item1);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(PK id, [FromBody] UpdateReq request)
        {
            var result = await service.UpdateAsync(id, request);
            return this.HandleResult(result.Item1);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(PK id)
        {
            var result = await service.DeleteAsync(id);
            return this.HandleResult(result);
        }
    }
}
