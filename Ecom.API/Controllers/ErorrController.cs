using Ecom.API.Halper;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("errors/[statusCode]")]
    [ApiController]
    public class ErorrController : ControllerBase
    {
        [HttpGet]
        public IActionResult Erorr(int statusCode)
        {
            return new ObjectResult(new ResponseApi(statusCode));
        }
    }
}
