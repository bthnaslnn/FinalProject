using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
            => Ok(User?.Claims?.Select(c => new { c.Type, c.Value }));
    }
}
