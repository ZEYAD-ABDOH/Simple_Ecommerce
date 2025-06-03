using AutoMapper;
using Ecom.API.Halper;
using Ecom.Core.DTO;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ecom.API.Controllers
{
    public class ProductsController : BaseController
    {

        public ProductsController(IUnitOfWord work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all-Product")]
        public async Task<IActionResult> get(string sort)
        {
            try
            {
                var product = await _work.ProdutRepositry.GetAllAsync(sort);

                if (product.IsNullOrEmpty())
                {
                    BadRequest(new ResponseApi(400));

                }


                return Ok(product);
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var product = await _work.ProdutRepositry.GetByIdAsync(id,
                   x => x.Category,
                   x => x.Photos
                    );
                var result = _mapper.Map<ProductDTO>(product);

                if (product is null) return BadRequest(new ResponseApi(400));

                return Ok(result);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }


        //[HttpPost("add-Product")]
        //public async Task<IActionResult> add(AddProductDTO productDTO)
        //{
        //    //await _work.ProdutRepositry.AddAsync(productDTO);
        //    return Ok(new ResponseApi(200));
        //    //try
        //    //{


        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    return BadRequest(new ResponseApi(400, ex.Message));
        //    //}
        //}
    }
}
