using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Response;

namespace IG.Application.Core.Interfaces.Crud
{
    public interface IDeleteService<PK>
    {
        Task<Response<bool>> DeleteAsync(
            PK id,
            bool safeDelete = false,
            string deletedBy = "SYSTEM"
        );
    }
}
