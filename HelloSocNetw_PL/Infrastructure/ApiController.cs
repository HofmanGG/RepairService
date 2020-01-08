using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HelloSocNetw_PL.Validators;

namespace HelloSocNetw_PL.Infrastructure
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public abstract class ApiController: ControllerBase
    {
        
    }
}
