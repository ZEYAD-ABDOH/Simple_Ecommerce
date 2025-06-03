using AutoMapper;
using Ecom.API.Halper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Products;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class CategoryController(IUnitOfWord work, IMapper mapper) : BaseController(work, mapper)
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var categories = await work.CategoryRepositry.GetAllAsync();
                if (categories is null) return BadRequest(new ResponseApi(400));
                return Ok(categories);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var category = await work.CategoryRepositry.GetByIdAsync(id);
                if (category is null) return BadRequest(new ResponseApi(400, $"not found categroy id ={id}"));
                return Ok(category);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> add(CategoryDTO categoryDTO)
        {
            try
            {
                var categroy = _mapper.Map<Category>(categoryDTO);
                await _work.CategoryRepositry.AddAsync(categroy);
                return Ok(

                    new ResponseApi(200, "Item has been added ")
                );
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-category")]
        public async Task<IActionResult> Update(UpdateCategoryDTO categoryDTO)
        {
            try
            {
                var categroy = _mapper.Map<Category>(categoryDTO);
                await _work.CategoryRepositry.UpdateAsync(categroy);
                return Ok(
                     new ResponseApi(200, " Item has been Update ")
                );
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-categroy/{id}")]

        public async Task<IActionResult> delete(int id)
        {
            try
            {
                await work.CategoryRepositry.DeleteAsync(id);

                return Ok(
                       new ResponseApi(200, " Item has been deleted ")
                  );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

