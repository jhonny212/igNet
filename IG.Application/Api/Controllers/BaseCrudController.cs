using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IG.Application.Core.Extensions;
using IG.Application.Core.Interfaces.Crud;
using IG.Application.Core.Request;
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
        where CreateReq : BaseRequest
        where UpdateReq : BaseRequest
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
            try
            {
                var result = await service.CreateAsync(request);
                return this.HandleResult(result.Item1);
            }
            catch (Exception ex)
            {
                return this.ServerError(ex);
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(PK id, [FromBody] UpdateReq request)
        {
            try
            {
                var result = await service.UpdateAsync(id, request);
                return this.HandleResult(result.Item1);
            }
            catch (Exception ex)
            {
                return this.ServerError(ex);
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(PK id)
        {
            try
            {
                var result = await service.DeleteAsync(id);
                return this.HandleResult(result);
            }
            catch (Exception ex)
            {
                return this.ServerError(ex);
            }
        }
    }
}
