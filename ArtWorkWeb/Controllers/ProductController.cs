using ArtWorkWeb.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ArtWorkWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("allartworkuser/{id}")]
        public IActionResult GetAllProductUser(int id)
        {
            var reponse = _productService.GetAllProductUser(id);
            if (reponse.Key.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(reponse.Key);
            }
            return Ok(reponse.Value);
        }
        [HttpGet("image/{id}")]
        public IActionResult GetImage(int id)
        {
            var reponse = _productService.GetImage(id);
            if (reponse.Key.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                return NotFound(reponse.Key);
            }
            return Ok(reponse.Value);
        }

        [HttpGet("HotArtWork")]
        public IActionResult GetHotProduct()
        {
            var reponse = _productService.GetHotProduct();
            return Ok(reponse.Value);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var (message, response) = _productService.GetProduct(id);

            if (response == null)
            {
                return NotFound(message);
            }

            return Ok(response);
        }

    }
}
