using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;
using System.Linq.Expressions;

namespace ProductApi.Controllers
{
    [Route("api/[Controller]")]
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<Product>>> GetAll()
        {
            try
            {
                return Ok((await _productService.FindAll()).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByID([FromRoute]int id)
        {
            try
            {
                Expression<Func<Product, bool>> expression = p => p.Id == id;
                return Ok((await _productService.FindByCondition(expression)).ToList()?.First());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductById([FromRoute] int id, [FromBody] Product product)
        {
            Expression<Func<Product, bool>> expression = p => p.Id == id;
            Product? p = (await _productService.FindByCondition(expression)).FirstOrDefault() ?? null;

            if (p == null)
            {
                return NotFound();
            }

            try
            {
                if (product.Id != id)
                {
                    throw new Exception("product id is differnt");
                }
                await _productService.Update(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductById([FromRoute] int id)
        {
            Expression<Func<Product, bool>> expression = p => p.Id == id;
            Product? p = (await _productService.FindByCondition(expression)).FirstOrDefault() ?? null;


            if (p == null)
            {
                return NotFound();
            }

            try
            {
                await _productService.Delete(p);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody]Product p)
        {
            try
            {
                await _productService.Create(p);  
                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetProductByName/{name}")]
        public async Task<ActionResult<Product>> GetProductByName(string name)
        {
            try
            {
                Expression<Func<Product, bool>> expression = p => p.Name == name;
                return Ok((await _productService.FindByCondition(expression)).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
