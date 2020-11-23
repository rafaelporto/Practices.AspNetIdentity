using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet()]
        [SwaggerOperation(
            Summary = "Home",
            Description = "Welcome message",
            OperationId = "home.index",
            Tags = new[] { "Home" })
        ]
        [Authorize]
        public IActionResult Index()
        {
            return Ok("Welcome to Practices.Identity!");
        }
    }
}
