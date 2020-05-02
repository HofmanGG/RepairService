using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HelloSocNetw_PL.Infrastructure
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public abstract class ApiController: ControllerBase
    {
        
    }
}
