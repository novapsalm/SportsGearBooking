using Backend.Models;
using Backend.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("policy")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                return Ok(product);
            }
        
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProduct(id, product);
                return Ok(updatedProduct);
            }
     
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST: api/Products
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            try
            { 
                var success = _productService.AddProduct(product);

                return Ok(success);
            }
   
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var deletedProduct = await _productService.DeleteProduct(id);
                return Ok(deletedProduct);
            }
            catch (Exception)
            {
                return NotFound($"Product with ID {id} not found.");
            }
        /*    catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }*/
        }

        [HttpGet("search/{searchItem}")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProductByNameOrDesc(string searchItem)
        {
            var products = await _productService.SearchProduct(searchItem);
            return Ok(products);
        }

        [HttpGet("categoryId/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                var products = await _productService.GetProductsByCategory(categoryId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
           /* catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }*/
        }
/*
        [HttpGet("expired")]
        public async Task<ActionResult<IEnumerable<Product>>> GetExpiredProducts()
        {
            var expiredProducts = await _productService.GetExpiredProducts();
            return Ok(expiredProducts);
        }

        [HttpGet("expiring")]
        public async Task<ActionResult<IEnumerable<Product>>> GetExpiringProducts()
        {
            var expiringProducts = await _productService.GetExpiringProducts();
            return Ok(expiringProducts);
        }*/
    }
}
