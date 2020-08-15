using Microsoft.AspNetCore.Authorization;
using Pygma.Common.Constants;
using Pygma.Common.Models.Base;

namespace Pygma.Admin.Api.Base
{
    [Authorize(Roles = Roles.Admin)]
    public abstract class AdminControllerBase: CommonControllerBase
    {
        
    }
}