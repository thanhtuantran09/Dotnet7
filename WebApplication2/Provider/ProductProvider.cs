using WebApplication2.Models;
using WebApplication2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Provider
{
    public class ProductProvider
    {
        private readonly AppDbContext _dbContext;

        public ProductProvider(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetDataAsync()
        {
            return await _dbContext.products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await _dbContext.products
                         .Include(p => p.Category) // Include the related Category data
                         .FirstOrDefaultAsync(p => p.id == id);

            return product;
        }

        public async Task CreateAsync(Product product)
        {
            product.created_at = DateTimeOffset.UtcNow;

            _dbContext.products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            product.updated_at = DateTimeOffset.UtcNow;
            _dbContext.products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _dbContext.products.FindAsync(id);

            if (product == null)
            {
                // Product not found
                return;
            }

            var isProductInUse = await IsProductInUse(product);

            if (isProductInUse)
            {
                // Product is in use, throw an exception or handle as needed
                throw new InvalidOperationException("Product is in use and cannot be deleted.");
            }

            _dbContext.products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> IsProductInUse(Product product)
        {
            // Check if the product is associated with any category
            return await _dbContext.categories
         .Where(c => c.products != null)
         .AnyAsync(c => c.products.Any(p => p.id == product.id));
        }


    }
}
