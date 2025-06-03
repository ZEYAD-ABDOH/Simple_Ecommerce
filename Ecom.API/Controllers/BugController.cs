using AutoMapper;
using Ecom.Core.interfaces;

using Microsoft.AspNetCore.Mvc;
namespace Ecom.API.Controllers
{
    public class BugController : BaseController
    {
        public BugController(IUnitOfWord work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("not-found")]
        public async Task<IActionResult> GetNotFound()
        {
            var categroy = await _work.CategoryRepositry.GetByIdAsync(100);
            if (categroy is null) return NotFound();
            return Ok(categroy);

        }
        [HttpGet("server-error")]
        public async Task<IActionResult> GetServerError()
        {
            var categroy = await _work.CategoryRepositry.GetByIdAsync(100);
            categroy.Name = " ";
            return Ok(categroy);


        }
        [HttpGet("bad-request/{id}")]
        public async Task<IActionResult> GetBadBequest(int id)
        {
            return Ok();

        }
        [HttpGet("bad-request/")]
        public async Task<IActionResult> GetBadBequest()
        {
            return BadRequest();

        }
    }
}
