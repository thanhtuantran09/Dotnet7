using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Provider;

namespace WebApplication2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryProvider _categoryProvider;

        public CategoryController(CategoryProvider categoryProvider)
        {
            _categoryProvider = categoryProvider;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetData()
        {
            var data = await _categoryProvider.GetDataAsync();

            // Map the data to the desired JSON format
            var result = new
            {
                message = "ok",
                responseData = data.Select(category => new
                {
                    id = category.Id,
                    name = category.Name,
                    products = category.products?.Select(product => new
                    {
                        id = product.id,
                        code = product.code,
                        name = product.name,
                        brand = product.brand,
                        type = product.type,
                        description = product.description,
                        created_at = product.created_at,
                        updated_at = product.updated_at,
                    }).ToList()
                }).ToList(),
                status = "success",
                timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                violations = (object?)null // Explicitly cast null to object?
            };

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetCategory(int id)
        {
            var category = await _categoryProvider.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Map the data to the desired JSON format
            var result = new
            {
                message = "ok",
                responseData = new List<object>
        {
            new
            {
                id = category.Id,
                name = category.Name,
                // Use Products instead of products
                products = category.products?.Select(product => new
                {
                    id = product.id,
                    code = product.code,
                    name = product.name,
                    brand = product.brand,
                    type = product.type,
                    description = product.description,
                    created_at = product.created_at,
                    updated_at = product.updated_at,
                   
                }).ToList()
            }
        },
                status = "success",
                timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                violations = (object?)null
            };

            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("Invalid ID");
            }

            await _categoryProvider.UpdateAsync(category);

            // Map the data to the desired JSON format
            var result = new
            {
                message = "Category updated successfully",
                responseData = new
                {
                    id = category.Id,
                    name = category.Name,
                   
                },
                status = "success",
                timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                violations = (object?)null // Explicitly cast null to object?
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _categoryProvider.CreateAsync(category);
            var result = new
            {
                message = "Category created successfully",
                responseData = new
                {
                    id = category.Id,
                    name = category.Name,
                },
                status = "success",
                timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                violations = (object?)null // Explicitly cast null to object?
            };

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, result);
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!await CategoryExists(id))
            {
                return NotFound("Category not found");
            }

            await _categoryProvider.DeleteAsync(id);

            // Map the data to the desired JSON format
            var result = new
            {
                message = "Category deleted successfully",
                status = "success",
                timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                violations = (object?)null // Explicitly cast null to object?
            };

            return Ok(result);
        }
        private async Task<bool> CategoryExists(int id)
        {
            var category = await _categoryProvider.GetByIdAsync(id);
            return category != null;
        }
    }
}
