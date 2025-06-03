using AutoMapper;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWord _work;
        protected readonly IMapper _mapper;

        public BaseController(IUnitOfWord work, IMapper mapper)
        {
            _work = work;
            _mapper = mapper;
        }
    }
}
