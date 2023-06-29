using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using ProductApi.Services;
using ProductApi.Models;

namespace CategoryApi.Controllers
{
    [Route("api/[Controller]")]
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<Category>>> GetAll()
        {
            try
            {
                return Ok((await _categoryService.FindAll()).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryByID(int id)
        {
            try
            {
                Expression<Func<Category, bool>> expression = p => p.Id == id;
                return Ok((await _categoryService.FindByCondition(expression)).First());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoryById([FromRoute]int id, [FromBody] Category Category)
        {
            Expression<Func<Category, bool>> expression = c => c.Id == id;
            Category? c = (await _categoryService.FindByCondition(expression)).FirstOrDefault() ?? null;

            if (c == null)
            {
                return NotFound();
            }

            try
            {
                if (Category.Id != id)
                {
                    throw new Exception("Category id is differnt from id provided");
                }
                await _categoryService.Update(Category);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryById([FromRoute] int id)
        {
            Expression<Func<Category, bool>> expression = c => c.Id == id;
            Category? c = (await _categoryService.FindByCondition(expression)).FirstOrDefault() ?? null;

            if (c == null)
            {
                return NotFound();
            }

            try
            {
                await _categoryService.Delete(c);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromBody] Category c)
        {
            try
            {
                await _categoryService.Create(c);
                return Ok(c);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCategoryByName/{name}")]
        public async Task<ActionResult<Category>> GetCategoryByName(string name)
        {
            try
            {
                Expression<Func<Category, bool>> expression = c => c.Name == name;
                return Ok((await _categoryService.FindByCondition(expression)).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
