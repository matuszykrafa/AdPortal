using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementPortal.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public ActionResult Pong()
        {
            return Ok("pong");
        }
        [HttpGet("auth"), Authorize]
        public ActionResult PongAuth()
        {
            return Ok("pong auth");
        }
    }
}
