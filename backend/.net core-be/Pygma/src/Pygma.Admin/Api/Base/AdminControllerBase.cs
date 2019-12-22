using Microsoft.AspNetCore.Authorization;
using Pygma.Common.Models.Base;

namespace Pygma.Admin.Api.Base
{
    [Authorize]
    public abstract class AdminControllerBase: CommonControllerBase
    {
        
    }
}