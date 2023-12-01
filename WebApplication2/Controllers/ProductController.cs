using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Provider;

namespace WebApplication2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductProvider _productProvider;

        public ProductController(ProductProvider productProvider)
        {
            _productProvider = productProvider;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetData()
        {
            var data = await _productProvider.GetDataAsync();

            // Map the data to the desired JSON format
            var result = new
            {
                message = "ok",
                responseData = data.Select(product => new
                {
                    id = product.id,
                    code = product.code,
                    name = product.name,
                    brand = product.brand,
                    type = product.type,
                    description = product.description,
                    created_at = product.created_at,
                    updated_at = product.updated_at,
                    categoryId = product.CategoryId,
                    category = product.Category?.Name // Assuming you have a Name property in your Category class
                }).ToList(),
                status = "success",
                timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                violations = (object?)null // Explicitly cast null to object?
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetProduct(int id)
        {
            var product = await _productProvider.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            // Map the data to the desired JSON format
            var result = new
            {
                message = "ok",
                responseData = new
                {
                    id = product.id,
                    code = product.code,
                    name = product.name,
                    brand = product.brand,
                    type = product.type,
                    description = product.description,
                    created_at = product.created_at,
                    updated_at = product.updated_at,
                    categoryId = product.CategoryId,
                    category = product.Category?.Name
                },
                status = "success",
                timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                violations = (object?)null // Explicitly cast null to object?
            };

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutProduct(int id, Product product)
        {
            if (id != product.id)
            {
                return BadRequest("Invalid ID");
            }

            await _productProvider.UpdateAsync(product);

            // Map the data to the desired JSON format
            var result = new
            {
                message = "Product updated successfully",
                responseData = new
                {
                    id = product.id,
                    code = product.code,
                    name = product.name,
                    brand = product.brand,
                    type = product.type,
                    description = product.description,
                    created_at = product.created_at,
                    updated_at = product.updated_at,
                    categoryId = product.CategoryId,
                    category = product.Category?.Name
                },
                status = "success",
                timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                violations = (object?)null // Explicitly cast null to object?
            };

            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<object>> PostProduct(Product product)
        {
            await _productProvider.CreateAsync(product);

            // Map the data to the desired JSON format
            var result = new
            {
                message = "Product created successfully",
                responseData = new
                {
                    id = product.id,
                    code = product.code,
                    name = product.name,
                    brand = product.brand,
                    type = product.type,
                    description = product.description,
                    created_at = product.created_at,
                    updated_at = product.updated_at,
                    categoryId = product.CategoryId,
                    category = product.Category?.Name
                },
                status = "success",
                timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                violations = (object?)null // Explicitly cast null to object?
            };

            return CreatedAtAction(nameof(GetProduct), new { id = product.id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteProduct(int id)
        {
            if (!await ProductExists(id))
            {
                return NotFound("Product not found");
            }

            try
            {
                await _productProvider.DeleteAsync(id);

                // Map the data to the desired JSON format
                var result = new
                {
                    message = "Product deleted successfully",
                    status = "success",
                    timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                    violations = (object?)null // Explicitly cast null to object?
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Handle exception if necessary
                return StatusCode(500, $"An error occurred while deleting the product: {ex.Message}");
            }
        }

        private async Task<bool> ProductExists(int id)
        {
            var product = await _productProvider.GetByIdAsync(id);
            return product != null;
        }

    }
}

