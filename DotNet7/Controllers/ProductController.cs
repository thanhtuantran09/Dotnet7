using DotNet7.Data;
using DotNet7.Models;
using DotNet7.Provider;
using Microsoft.AspNetCore.Mvc;

using System.Linq;
namespace DotNet7.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductProvider _productProvider;

        public ProductController(ProductProvider productProvider)
        {
            _productProvider = productProvider;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetData()
        {
            var data = await _productProvider.GetDataAsync();
            return Ok(data);
        }

    }
}
