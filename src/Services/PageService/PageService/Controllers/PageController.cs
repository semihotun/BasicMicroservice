using Application.Handlers.Page.Commands.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Setup.ActionFilter;
using System.Threading.Tasks;

namespace PageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : BaseController
    {
        [AuthorizeControl]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("cratepage")]
        public async Task<IActionResult> CreatePage([FromForm]CreatePageCommand command)
        {
            var result=await Mediator.Send(command);

            return ToResult(result);
        }
            
    }
}
