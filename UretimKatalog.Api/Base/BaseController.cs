using Microsoft.AspNetCore.Mvc;
using UretimKatalog.Api.Models;

namespace UretimKatalog.Api.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(ApiResponse<T> response)
        {
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}
