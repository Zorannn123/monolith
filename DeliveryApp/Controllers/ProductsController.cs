using DeliveryApp.DTO.ProductDTO;
using DeliveryApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        [Route("addProduct")]
        [Authorize(Roles = "Admin")]
        public ActionResult Post([FromBody] ProductDto product)
        {
            if (productService.AddEntity(product))
                return Ok(true);

            return BadRequest(false);
        }

        [HttpGet]
        [Route("getAll")]
        public ActionResult GetAllProducts()
        {
            try
            {
                return Ok(productService.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("deleteProduct/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteProduct(long id)
        {
            if (productService.RemoveEntity(id))
                return Ok(true);
            return BadRequest(false);
        }
    }   
}
