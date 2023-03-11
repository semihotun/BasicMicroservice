using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteMapService.SeedWork;

namespace SiteMapService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteMapController : ControllerBase
    {
        private readonly IRepository _repo;

        [NonAction]
        protected IActionResult Index()
        {
            return Ok("sadas");
        }

    }
}
